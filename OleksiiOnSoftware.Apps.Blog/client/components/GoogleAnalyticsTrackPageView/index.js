import React from 'react'
import { withRouter } from 'react-router'

@withRouter
export class GoogleAnalyticsTrackPageView extends React.Component {
  componentDidUpdate (prevProps) {
    if (this.props.location !== prevProps.location) {
      ga('set', 'page', this.props.location.pathname)
      ga('send', 'pageview')
    }
  }

  render () {
    return false
  }
}
