import React from 'react'

import { Icon, Label } from 'semantic-ui-react'

// TODO: Refactor URL
export class PostComments extends React.Component {
  render () {
    return (
      <Label>
        <Icon name='comments' /> &nbsp; <a href={`http://oleksiionsoftware.com/post/${this.props.url}#disqus_thread`}></a>
      </Label>
    )
  }
}
