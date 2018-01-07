/* @flow */

// Framework
import React from 'react'

// UI
import {
  Breadcrumb,
  Icon
} from 'semantic-ui-react'

import type { Filter } from 'types'

// Types
type Props = {
  filter: Filter,
  onHomeClick: () => void
}

// Styles
const styles = {
  removeIconStyle: {
    cursor: 'pointer'
  }
}

// Components
export class Header extends React.Component<Props> {
  static defaultProps = {
    onHomeClick: () => { }
  }

  handleHomeClick = () => {
    this.props.onHomeClick()
  }

  getSections () {
    const breadcrumb = []

    // Push "Home" node into breadcrumb
    if (this.props.filter.by === 'none') {
      breadcrumb.push({ key: 'home', content: 'Home', link: false, active: true })
      return breadcrumb
    } else {
      breadcrumb.push({ key: 'home', content: 'Home', link: true, active: false, onClick: this.handleHomeClick })
    }

    // Push the category into breadcrumb
    switch (this.props.filter.by) {
      case 'date':
        breadcrumb.push({ key: 'date', content: 'Date', link: false, active: false })
        break
      case 'category':
        breadcrumb.push({ key: 'category', content: 'Category', link: false, active: false })
        break
      case 'tag':
        breadcrumb.push({ key: 'tag', content: 'Tag', link: false, active: false })
        break
      default:
        break
    }

    // Push currently selected filter title
    breadcrumb.push({
      key: 'item',
      link: false,
      active: true,
      content: (
        <div>
          {this.props.filter.title}
          <a style={styles.removeIconStyle} >
            <Icon name='delete' onClick={this.handleHomeClick} />
          </a>
        </div>
      )
    })

    return breadcrumb
  }

  render () {
    return (
      <Breadcrumb icon='right angle' sections={this.getSections()} />
    )
  }
}
