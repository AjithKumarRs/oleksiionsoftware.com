// React
import React from 'react'
import { connect } from 'react-redux'
import { init } from 'decorators'

// Actions
import { categoryInit } from 'actions'

@init(ctx => ctx.store.dispatch(categoryInit(ctx.hostname, ctx.match.params.categoryId)))
@connect(
  (state) => ({
    isInited: state.category.isInited,
    brand: state.category.brand,
    links: state.category.links,
    posts: state.category.posts
  }),
  (dispatch) => ({
    init: (hostname, categoryId) => dispatch(categoryInit(hostname, categoryId))
  })
)
export class Category extends React.Component {
  render () {
    return (
      <div>
        <div>
          CATEGORY: {this.props.match.params.categoryId}
        </div>
        <div>
          IsInited: {this.props.isInited ? 'true' : 'false'}
        </div>
      </div>
    )
  }
}
