/* @flow */

// React
import React from 'react'

// UI
import { Item } from 'semantic-ui-react'
import ReactDisqusThread from 'react-disqus-thread'

// Custom UI
import {
  PostDate,
  PostCategory,
  PostTag,
  PostComments
} from 'components'

// Types
import type { Post, Tag, Category } from 'types'

type Props = {
  post: Post,
  onHeaderClick: (post: Post) => void,
  onDateClick: (date: string) => void,
  onCategoryClick: (category: Category) => void,
  onTagClick: (tag: Tag) => void
}

// Components
export class ShortPost extends React.Component<Props> {
  static defaultProps = {
    onHeaderClick: () => { },
    onDateClick: () => { },
    onCategoryClick: () => { },
    onTagClick: () => { }
  }

  handleHeaderClick = (post: Post) => {
    this.props.onHeaderClick(post)
  }

  handleDateClick = (date: string) => {
    this.props.onDateClick(date)
  }

  handleCategoryClick = (category: Category) => {
    this.props.onCategoryClick(category)
  }

  handleTagClick = (tag: Tag) => {
    this.props.onTagClick(tag)
  }

  render () {
    return (
      <Item key={this.props.post.id}>
        <Item.Content>
          <Item.Header as='a' onClick={() => this.handleHeaderClick(this.props.post)}>{this.props.post.title}</Item.Header>
          <Item.Meta>
            {this.props.post.date && <PostDate onClick={this.handleDateClick} date={this.props.post.date} />}
            {this.props.post.category && <PostCategory onClick={this.handleCategoryClick} category={this.props.post.category} />}
            {this.props.post.tags && this.props.post.tags.map(tag => <PostTag key={tag.id} onClick={this.handleTagClick} tag={tag} />)}
            <PostComments url={this.props.post.id} />
          </Item.Meta>
          <Item.Description>
            <div dangerouslySetInnerHTML={{ __html: this.props.post.short }}>
            </div>
          </Item.Description>
          <Item.Extra>
            <a onClick={() => this.handleHeaderClick(this.props.post)}>Read more...</a>
          </Item.Extra>
        </Item.Content>
      </Item>
    )
  }
}
