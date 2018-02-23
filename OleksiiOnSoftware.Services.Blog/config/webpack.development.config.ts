import * as webpack from "webpack";
import * as path from "path";
import { TsConfigPathsPlugin } from "awesome-typescript-loader";
import { Plugin } from "tapable";

// Set environment variables
process.env.NODE_ENV = process.env.NODE_ENV || "development";
process.env.NODE_HOSTNAME = process.env.NODE_HOSTNAME || "localhost";
process.env.NODE_PORT = process.env.NODE_PORT || "3000";
process.env.API_PUBLIC_HOSTNAME = process.env.API_PUBLIC_HOSTNAME || "localhost";
process.env.API_INTERNAL_HOSTNAME = process.env.API_INTERNAL_HOSTNAME || "localhost";

const context = path.resolve(__dirname, "../");

// Webpack config
const config: webpack.Configuration = {
  devtool: "inline-source-map",
  entry: [
    "webpack-hot-middleware/client",
    path.resolve(context, "client/client-render.tsx")
  ],
  output: {
    path: path.resolve(context, "dist"),
    filename: "bundle.js",
    publicPath: "/dist/"
  },
  plugins: [
    new webpack.HotModuleReplacementPlugin(),
    new webpack.NoEmitOnErrorsPlugin(),
    new webpack.EnvironmentPlugin([
      "NODE_ENV",
      "NODE_HOSTNAME",
      "NODE_PORT",
      "API_PUBLIC_HOSTNAME",
      "API_INTERNAL_HOSTNAME"
    ]),
    new TsConfigPathsPlugin() as Plugin
  ],
  resolve: {
    extensions: [
      ".config.js",
      ".config.ts",
      ".js",
      ".jsx",
      ".ts",
      ".tsx",
      ".css"
    ],
    alias: {
      request: "browser-request",
      components: path.resolve(context, "./client/components"),
      containers: path.resolve(context, "./client/containers"),
      actions: path.resolve(context, "./client/actions"),
      reducers: path.resolve(context, "./client/reducers"),
      store: path.resolve(context, "./client/store"),
      decorators: path.resolve(context, "./client/decorators"),
      routes: path.resolve(context, "./client/routes"),
      utils: path.resolve(context, "./client/utils"),
      types: path.resolve(context, "./client/types")
    }
  },
  module: {
    rules: [
      // ES and RHL
      {
        test: /\.tsx?$/,
        exclude: /node_modules/,
        use: [
          { loader: "react-hot-loader" },
          { loader: "babel-loader" },
          { loader: "awesome-typescript-loader" }
        ]
      },

      // Pre
      {
        enforce: "pre",
        test: /\.js$/,
        loader: "source-map-loader"
      },

      // JSON
      {
        test: /\.json$/,
        exclude: /node_modules/,
        use: "json-loader"
      },

      // CSS
      {
        test: /\.css$/,
        use: [
          {
            loader: "style-loader"
          },
          {
            loader: "css-loader",
            options: {
              localIdentName: "[path][name]-[local]-[hash:base64:5]",
              modules: true,
              sourceMap: true
            }
          }
        ]
      }
    ]
  }
};

export default config;
