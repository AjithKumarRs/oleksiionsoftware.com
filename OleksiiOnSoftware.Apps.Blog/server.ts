import * as chokidar from "chokidar";
import * as cssModulesRequireHook from "css-modules-require-hook";
import * as express from "express";
import * as http from "http";
import * as webpack from "webpack";
import * as webpackDevMiddleware from "webpack-dev-middleware";
import * as webpackHotMiddleware from "webpack-hot-middleware";
import * as bodyParser from "body-parser";

// Setup css modules naming convention compilation on server side
cssModulesRequireHook({
  generateScopedName: "[path][name]-[local]-[hash:base64:5]",
  extensions: [".css"]
});

// Setup express to serve static files
const app = express();

// Serve static files
app.use(express.static("static"));
app.use("/dist", express.static("dist"));

// Set up body-parser
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

// Enable HMR in development mode
if (process.env.NODE_ENV === "development") {
  const config = require("./config/webpack.development.config").default;
  const compiler = webpack(config);

  // Serve hot-reloading bundle to client
  app.use(
    webpackDevMiddleware(compiler, {
      publicPath: config.output.publicPath,
      watchOptions: {
        aggregateTimeout: 300,
        poll: 1000,
        ignored: /node_modules/
      }
    })
  );

  app.use(webpackHotMiddleware(compiler));

  // Do "hot-reloading" of express stuff on the server
  // Throw away cached modules and re-require next time
  // Ensure there's no important state in there!
  const watcher = chokidar.watch("./server");
  watcher.on("ready", function () {
    watcher.on("all", function () {
      console.log("Clearing /server/ module cache from server");
      Object.keys(require.cache).forEach(function (id) {
        if (/[/\\]server[/\\]/.test(id)) delete require.cache[id];
      });
    });
  });

  // Do "hot-reloading" of react stuff on the server
  // Throw away the cached client modules and let them be re-required next time
  compiler.plugin("done", function () {
    console.log("Clearing /client/ module cache from server");
    Object.keys(require.cache).forEach(function (id) {
      if (/[/\\]client[/\\]/.test(id)) delete require.cache[id];
    });
  });
}

// Anything else gets passed to the client app's server rendering
app.get("*", function (req, res, next) {
  require("./client/server-render")(req, res, next);
});

const server = http.createServer(app);
server.listen(
  parseInt(process.env.NODE_PORT),
  process.env.NODE_HOSTNAME,
  function (err: Error) {
    if (err) throw err;

    const addr = server.address();

    console.log(
      "[%s]: Listening at http://%s:%d",
      process.env.NODE_ENV,
      addr.address,
      addr.port
    );
  }
);
