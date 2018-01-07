/* @flow */

// Framework
import React from 'react'

// UI
import { Icon, Label } from 'semantic-ui-react'

// Types
type Category = {
  id: string,
  title: string
}

type Props = {
  category: Category,
  onClick: (category: Category) => void
}

// Components
export class PostCategory extends React.Component<Props> {
  static defaultProps = {
    onClick: () => { }
  }

  handleClick = () => {
    this.props.onClick(this.props.category)
  }

  render () {
    return (
      <Label as='a' onClick={this.handleClick}>
        <Icon name='folder' /> &nbsp; {this.props.category.title}
      </Label>
    )
  }
}
