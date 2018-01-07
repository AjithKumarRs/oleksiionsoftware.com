import chokidar from 'chokidar'
import webpackConfig from './webpack.config'
import cssModulesRequireHook from 'css-modules-require-hook'
import express from 'express'
import http from 'http'
import webpack from 'webpack'
import webpackDevMiddleware from 'webpack-dev-middleware'
import webpackHotMiddleware from 'webpack-hot-middleware'
import bodyParser from 'body-parser'

// Setup css modules naming convention and SASS compilation on server side
cssModulesRequireHook({
  generateScopedName: '[path][name]-[local]-[hash:base64:5]',
  extensions: ['.css']
})

const compiler = webpack(webpackConfig)
const app = express()

// Serve static files
app.use(express.static('static'))

// Set up body-parser
app.use(bodyParser.urlencoded({ extended: true }))
app.use(bodyParser.json())

// Serve hot-reloading bundle to client
app.use(webpackDevMiddleware(compiler, {
  publicPath: webpackConfig.output.publicPath
}))

app.use(webpackHotMiddleware(compiler))

// Include server routes as a middleware
app.use(function (req, res, next) {
  require('./server/app')(req, res, next)
})

// Anything else gets passed to the client app's server rendering
app.get('*', function (req, res, next) {
  require('./client/server-render')(req, res, next)
})

// Do "hot-reloading" of express stuff on the server
// Throw away cached modules and re-require next time
// Ensure there's no important state in there!
const watcher = chokidar.watch('./server')
watcher.on('ready', function () {
  watcher.on('all', function () {
    console.log('Clearing /server/ module cache from server')
    Object.keys(require.cache).forEach(function (id) {
      if (/[/\\]server[/\\]/.test(id)) delete require.cache[id]
    })
  })
})

// Do "hot-reloading" of react stuff on the server
// Throw away the cached client modules and let them be re-required next time
compiler.plugin('done', function () {
  console.log('Clearing /client/ module cache from server')
  Object.keys(require.cache).forEach(function (id) {
    if (/[/\\]client[/\\]/.test(id)) delete require.cache[id]
  })
})

const server = http.createServer(app)
server.listen(process.env.NODE_PORT, process.env.NODE_HOSTNAME, function (err) {
  if (err) throw err

  const addr = server.address()

  console.log('Listening at http://%s:%d', addr.address, addr.port)
})
