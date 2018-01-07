import React from 'react'
import { withRouter } from 'react-router'

@withRouter
export class ScrollToTop extends React.Component {
  componentDidUpdate (prevProps) {
    if (this.props.location !== prevProps.location) {
      window.scrollTo(0, 0)
      ga('set', 'page', this.props.location.pathname);
      ga('send', 'pageview');

    }
  }

  render () {
    return false
  }
}
