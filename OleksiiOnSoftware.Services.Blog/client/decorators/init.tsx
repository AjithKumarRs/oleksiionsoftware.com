import * as React from "react";
import * as PropTypes from "prop-types";
import { RouteComponentProps, match } from "react-router";
import { wrapDisplayName, hoistStatics } from "recompose";
import { RootState } from "types";
import { Store } from "react-redux";

/**
 * Provide callback that will be called when component rendered on the server-side
 * or mounted on client side
 * @param {function} callback
 */
export const init = (callback: any) => (WrappedComponent: any) => {
  class WithInit extends React.Component<RouteComponentProps<any>> {
    // Ask React to pass Redux store through context
    static contextTypes = {
      store: PropTypes.object.isRequired
    };

    static init: (store: Store<RootState>, match: match<any>) => void;
    static displayName: string;

    constructor(props: RouteComponentProps<any>) {
      super(props);
    }

    /**
     * This method will be invoked after location change on router.
     * We call init callback to refresh data asociated with the component
     * @param {*} nextProps
     */
    componentWillReceiveProps(nextProps: RouteComponentProps<any>) {
      if (nextProps.location !== this.props.location) {
        callback(this.context.store, nextProps.match);
      }
    }

    /**
     * This method will be called ONLY on the client side after the first rendering
     */
    componentDidMount() {
      callback(this.context.store, this.props.match);
    }

    render() {
      return <WrappedComponent {...this.props} />;
    }
  }

  WithInit.init = callback;
  WithInit.displayName = wrapDisplayName(WrappedComponent, "SSR");

  return WithInit;
};
