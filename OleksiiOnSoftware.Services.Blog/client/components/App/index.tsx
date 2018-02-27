// Framework
import * as React from "react";

// Routing
import { Route, Switch, RouteProps } from "react-router";

// Custom UI
import { ScrollToTop, GoogleAnalyticsTrackPageView } from "components";

// Types
interface Props {
  routes: RouteProps[];
}

/**
 * Root element of the application.
 * It renders a set of static top-level routes and makes sure that only one top-level route can be selected at the same time.
 */
export class App extends React.Component<Props> {
  render() {
    return (
      <div>
        <ScrollToTop />
        <GoogleAnalyticsTrackPageView />

        <Switch>
          {this.props.routes.map(r => <Route key={r.path} {...r} />)}
        </Switch>
      </div>
    );
  }
}
