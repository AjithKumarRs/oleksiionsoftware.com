// Polyfills
import "url-search-params-polyfill";

// Infrastructure
import * as React from "react";
import { render } from "react-dom";
import { Provider } from "react-redux";
import { ConnectedRouter } from "react-router-redux";
import createHistory from "history/createBrowserHistory";

// App
import { App, DevTools } from "components";
import configureStore from "store";
import routes from "routes";

// Use browser history
const history = createHistory();

declare var window: {
  initialStoreData: any;
  dev: any;
};

declare var document: any;

// Load the default store state from server-side
const store = configureStore(history as any, window.initialStoreData);
window.dev = { store };

// Render the app
render(
  <Provider store={store}>
    <ConnectedRouter store={store} history={history}>
      <App routes={routes} />
    </ConnectedRouter>
  </Provider>,
  document.getElementById("root")
);

// Render the dev tools in dev mode
if (process.env.NODE_ENV === "development") {
  render(<DevTools store={store} />, document.getElementById("tools"));
}
