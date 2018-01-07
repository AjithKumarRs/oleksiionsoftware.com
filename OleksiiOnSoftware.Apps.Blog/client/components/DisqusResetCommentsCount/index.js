import React from 'react'
import { withRouter } from 'react-router'

@withRouter
export class DisqusResetCommentsCount extends React.Component {
  componentDidUpdate (prevProps) {
    if (window.DISQUSWIDGETS) {
      window.DISQUSWIDGETS.getCount({ reset: true })
    }
  }

  componentDidMount () {
    if (window.DISQUSWIDGETS) {
      window.DISQUSWIDGETS.getCount({ reset: true })
    }
  }

  render () {
    return false
  }
}
