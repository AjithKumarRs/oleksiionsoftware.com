import * as React from "react";
import { RouteComponentProps, withRouter } from "react-router";

declare var DISQUSWIDGETS: any;

class DisqusResetCommentsCountImpl extends React.Component<
  RouteComponentProps<any>
> {
  componentDidUpdate() {
    if (typeof DISQUSWIDGETS !== "undefined") {
      DISQUSWIDGETS.getCount({ reset: true });
    }
  }

  componentDidMount() {
    if (typeof DISQUSWIDGETS !== "undefined") {
      DISQUSWIDGETS.getCount({ reset: true });
    }
  }

  render() {
    return false;
  }
}

const DisqusResetCommentsCount = withRouter(DisqusResetCommentsCountImpl);
export { DisqusResetCommentsCount };
