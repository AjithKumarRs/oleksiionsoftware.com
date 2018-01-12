import * as React from "react";
import { RouteComponentProps, withRouter } from "react-router";

declare var ga: any;
declare var window: any;

class ScrollToTopImpl extends React.Component<RouteComponentProps<any>> {
  componentDidUpdate(prevProps: RouteComponentProps<any>) {
    if (this.props.location !== prevProps.location) {
      window.scrollTo(0, 0);
      ga("set", "page", this.props.location.pathname);
      ga("send", "pageview");
    }
  }

  render() {
    return false;
  }
}

const ScrollToTop = withRouter(ScrollToTopImpl);
export { ScrollToTop };
