/* @flow */

// Framework
import * as React from 'react'

// UI
import { Menu } from 'semantic-ui-react'

// Styles
const styles = {
  pagination: {
    textAlign: 'center',
    margin: 0,
    padding: 0
  }
}

// Constants
const controls = {
  back: {
    name: '← Older',
    key: '-1'
  },

  next: {
    name: 'Newer →',
    key: '+1'
  }
}

// Types
type Props = {
  pageIndex: number,
  pageSize: number,
  pagesCount: number,
  onPageChanged: (pageIndex: number, pageSize: number) => void
}

// Components
export class Pagination extends React.Component<Props> {
  static defaultProps = {
    pageIndex: 0,
    pageSize: 10,
    onPageChanged: () => { }
  }

  handleBackClick = () => {
    if (this.props.pageIndex > 0) {
      this.props.onPageChanged(this.props.pageIndex - 1, this.props.pageSize)
    }
  }

  handleItemClick = (pageIndex: number) => {
    this.props.onPageChanged(pageIndex, this.props.pageSize)
  }

  handleNextClick = () => {
    if (this.props.pageIndex < this.props.pagesCount - 1) {
      this.props.onPageChanged(this.props.pageIndex + 1, this.props.pageSize)
    }
  }

  render () {
    const pages = []
    for (let i = 0; i < this.props.pagesCount; i++) {
      pages.push(<Menu.Item key={i} active={i === this.props.pageIndex} name={(i + 1).toString()} onClick={this.handleItemClick.bind(this, i)} />)
    }

    return (
      <div style={styles.pagination}>
        <Menu pagination borderless>
          <Menu.Item {...controls.back} disabled={this.props.pageIndex === 0} onClick={this.handleBackClick} />
          {pages}
          <Menu.Item {...controls.next} disabled={this.props.pageIndex === this.props.pagesCount - 1} onClick={this.handleNextClick} />
        </Menu>
      </div>
    )
  }
}
