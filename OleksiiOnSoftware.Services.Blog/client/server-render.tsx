// Polyfills
import "url-search-params-polyfill";

// Infrastructure
import * as React from "react";
import { Request, Response } from "express";
import { Provider } from "react-redux";
import { renderToString } from "react-dom/server";
import { matchPath } from "react-router-dom";
import { ConnectedRouter } from "react-router-redux";
import createMemoryHistory from "history/createMemoryHistory";

// App
import { App, NotFound } from "components";
import configureStore from "store";
import routes from "./routes";

module.exports = async function (req: Request, res: Response) {
  // Find suitable route for the request
  let match, component;
  const isMatched = routes.some(r => {
    const m = matchPath(req.url, r);
    if (m) {
      component = r.component;
      match = m;
      return true;
    }

    return false;
  });

  // If no routes found render NotFound
  if (!isMatched) {
    component = <NotFound />;
  }

  const history = createMemoryHistory({
    initialEntries: [req.path]
  });

  const store = configureStore(history as any, {
    home: {
      isInited: false,
      pageIndex: 0,
      pageSize: 10,
      postsCount: 0,
      pagesCount: 0,
      brand: "",
      copyright: "",
      posts: [],
      filter: {
        by: "none"
      },
      links: []
    },
    post: {
      brand: "",
      avatar: "",
      comments: true,
      copyright: "",
      github: "",
      linkedin: "",
      twitter: "",
      url: "",
      links: [],
      body: "",
      short: "",
      category: {
        id: "",
        title: ""
      },
      date: "",
      id: "",
      infobar: false,
      tags: [],
      title: ""
    },
    config: {
      hostname: req.hostname,
      isSidebarOpen: true
    }
  });

  // If component is decorated with @init decorator, call initialization function
  // Make sure it is called synchronously, because otherwise render will be called before state is fully loaded
  if (component && (component as any).init) {
    await (component as any).init(store, match);
  }

  // Render component on the server side into string.
  // If no data have been loaded to the Redux store, then render component empty and let Redux load data on the client-side
  const rendered = renderToString(
    <Provider store={store}>
      <ConnectedRouter store={store} history={history}>
        <App routes={routes} />
      </ConnectedRouter>
    </Provider>
  ); 

  // Return a complete page with rendered component and redux store state
  res.send(`
    <html>
      <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>oleksiionsoftware.com</title>
        
        <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/semantic-ui/2.2.2/semantic.min.css" />
        <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/prism/1.11.0/themes/prism-okaidia.css" />
        <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/prism/1.11.0/plugins/line-numbers/prism-line-numbers.css" />
        <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/prism/1.11.0/plugins/command-line/prism-command-line.css" />

        <script id="dsq-count-scr" src="//oleksiionsoftware.disqus.com/count.js" async></script>
      </head>

      <body>
        <div id="root">${rendered}</div>
        <div id="tools"></div>
        <script type="text/javascript">
          window.initialStoreData = ${JSON.stringify(store.getState())};
        </script>
        <script src="/dist/bundle.js"></script>

        <script>
          (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
          (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
          m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
          })(window,document,'script','https://www.google-analytics.com/analytics.js','ga');

          ga('create', 'UA-85608460-1', 'auto');
          ga('send', 'pageview');
        </script> 

      </body>
    </html>`);
};
