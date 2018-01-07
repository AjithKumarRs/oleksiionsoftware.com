/* @flow */

// Framework
import { connect } from 'react-redux'
import { init } from 'decorators'
import _ from 'lodash'

// Actions
import {
  postInitAsync,
  push
} from 'actions'

// Custom UI
import { Post } from 'components'

// Types
import type { RootState, Tag, Category, Post as PostType, Link } from 'types'

type Props = {
  brand: string,
  links: Link[],
  post: PostType,
  match: {
    params: { postId: string }
  },

  goHome: () => void,
  filterByDate: (date: string) => void,
  filterByCategory: (category: Category) => void,
  filterByTag: (tag: Tag) => void
}

const PostConnected = connect(
  (state: RootState, ownProps: Props) => ({
    brand: state.post.brand,
    links: state.post.links,
    post: state.post
  }),
  (dispatch) => ({
    goHome: () => dispatch(push(`/`)),
    filterByDate: (date: string) => dispatch(push(`/posts/date/${date}`)),
    filterByCategory: (category: Category) => dispatch(push(`/posts/category/${category.id}`)),
    filterByTag: (tag: Tag) => dispatch(push(`/posts/tag/${tag.id}`))
  })
)(Post)

const PostInited = init(
  (store, match) => store.dispatch(postInitAsync(match.params.postId))
)(PostConnected)

export {
  PostInited as Post
}
