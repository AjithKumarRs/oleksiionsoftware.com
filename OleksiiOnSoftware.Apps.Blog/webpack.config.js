import webpack from 'webpack'
import path from 'path'

// Set environment variables
process.env.NODE_ENV = process.env.NODE_ENV || 'development'
process.env.NODE_HOSTNAME = process.env.NODE_HOSTNAME || 'localhost'
process.env.NODE_PORT = process.env.NODE_PORT || '3000'

// Webpack config
export default {
  devtool: 'cheap-module-eval-source-map',
  entry: [
    'webpack-hot-middleware/client',
    './client/client-render.js'
  ],
  output: {
    path: __dirname,
    filename: 'bundle.js',
    publicPath: '/'
  },
  plugins: [
    new webpack.optimize.OccurrenceOrderPlugin(),
    new webpack.HotModuleReplacementPlugin(),
    new webpack.NoEmitOnErrorsPlugin(),
    new webpack.EnvironmentPlugin([
      'NODE_ENV',
      'NODE_HOSTNAME',
      'NODE_PORT'
    ])
  ],
  resolve: {
    extensions: ['.js', '.jsx', '.css'],
    alias: {
      request: 'browser-request',
      components: path.resolve(__dirname, './client/components'),
      containers: path.resolve(__dirname, './client/containers'),
      actions: path.resolve(__dirname, './client/actions'),
      reducers: path.resolve(__dirname, './client/reducers'),
      store: path.resolve(__dirname, './client/store'),
      decorators: path.resolve(__dirname, './client/decorators'),
      routes: path.resolve(__dirname, './client/routes'),
      utils: path.resolve(__dirname, './client/utils'),
      types: path.resolve(__dirname, './client/types')
    }
  },
  module: {
    loaders: [{
      test: /\.js$/,
      loaders: ['react-hot-loader', 'babel-loader'],
      include: path.join(__dirname, 'client')
    }],

    rules: [
      // ES and RHL
      {
        test: /\.js$/,
        include: path.join(__dirname, 'client'),
        use: [
          { loader: 'react-hot-loader' },
          { loader: 'babel-loader' }
        ]
      },

      // JSON
      {
        test: /\.json$/,
        include: path.join(__dirname, 'client'),
        use: 'json-loader'
      },

      // CSS
      {
        test: /\.css$/,
        include: path.join(__dirname, 'client'),
        use: [
          {
            loader: 'style-loader'
          },
          {
            loader: 'css-loader',
            options: {
              modules: true,
              localIdentName: '[path][name]-[local]-[hash:base64:5]',
              sourceMap: true
            }
          }
        ]
      }]
  }
}
