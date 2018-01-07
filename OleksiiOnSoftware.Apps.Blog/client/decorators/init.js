import React from 'react'
import PropTypes from 'prop-types'
import { wrapDisplayName, hoistStatics } from 'recompose'

/**
 * Provide callback that will be called when component rendered on the server-side
 * or mounted on client side
 * @param {function} callback
 */
export const init = (callback) => (WrappedComponent) => {
  class WithInit extends React.Component {
    // Ask React to pass Redux store through context
    static contextTypes = {
      store: PropTypes.object.isRequired
    };

    constructor (props) {
      super(props)

      this.displayName = `SSR(${WrappedComponent.displayName})`
    }

    /**
     * This method will be invoked after location change on router.
     * We call init callback to refresh data asociated with the component
     * @param {*} nextProps
     */
    componentWillReceiveProps (nextProps) {
      if (nextProps.location !== this.props.location) {
        callback(this.context.store, nextProps.match)
      }
    }

    /**
     * This method will be called ONLY on the client side after the first rendering
     */
    componentDidMount () {
      callback(this.context.store, this.props.match)
    }

    render () {
      return <WrappedComponent {...this.props} />
    }
  }

  WithInit.init = callback
  WithInit.displayName = wrapDisplayName(WrappedComponent, "SSR")

  // Copy all non-react static methods from wrapped component to resulting component
  hoistStatics(WithInit, WrappedComponent)
  return WithInit
}
