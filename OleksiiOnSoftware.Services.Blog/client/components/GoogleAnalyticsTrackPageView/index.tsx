import * as React from "react";
import { RouteComponentProps, withRouter } from "react-router";

declare var ga: any;

export class GoogleAnalyticsTrackPageViewImpl extends React.Component<
  RouteComponentProps<any>
> {
  componentDidUpdate(prevProps: RouteComponentProps<any>) {
    if (this.props.location !== prevProps.location) {
      ga("set", "page", this.props.location.pathname);
      ga("send", "pageview");
    }
  }

  render() {
    return false;
  }
}

const GoogleAnalyticsTrackPageView = withRouter(
  GoogleAnalyticsTrackPageViewImpl
);

export { GoogleAnalyticsTrackPageView };
