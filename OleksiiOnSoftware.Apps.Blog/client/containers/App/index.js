// Framework
import React from 'react'
import { Route, Switch } from 'react-router'

// Custom UI
import { ScrollToTop, GoogleAnalyticsTrackPageView } from 'components'

/**
 * Root element of the application.
 * It renders a set of static top-level routes and makes sure that only one top-level route can be selected at the same time.
 */
export class App extends React.Component {
  render () {
    return (
      <div>
        <ScrollToTop />
        <GoogleAnalyticsTrackPageView /> 
        
        <Switch>
          {this.props.routes.map(r => <Route {...r} />)}
        </Switch>
      </div>
    )
  }
}
