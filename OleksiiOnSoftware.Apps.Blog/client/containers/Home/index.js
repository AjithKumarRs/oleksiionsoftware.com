// @flow

// React
import { connect } from 'react-redux'
import { init } from 'decorators'

// Actions
import {
  push,
  toggleSidebar,
  homeInitAsync,
  homeFilterByCategoryAsync,
  homeFilterByTagAsync,
  homeFilterByDateAsync,
  homeChangePageAsync
} from 'actions'

// Custom UI
import { PostsList } from 'components'

// Types
import type { RootState, Dispatch, Category, Tag, Link } from 'types'

// Connect PostsList to Redux store
const HomeConnected = connect(
  (state: RootState) => ({
    isSidebarOpen: state.config.isSidebarOpen,
    brand: state.home.brand,
    filter: state.home.filter,
    links: state.home.links,
    posts: state.home.posts,
    postsCount: state.home.postsCount,
    pagesCount: state.home.pagesCount,
    pageIndex: state.home.pageIndex,
    pageSize: state.home.pageSize
  }),
  (dispatch: Dispatch) => ({
    toggleSidebar: () => dispatch(toggleSidebar()),
    openPost: (postId) => dispatch(push(`/post/${postId}`)),
    filterReset: () => dispatch(push('/')),
    filterByDate: (date: string) => dispatch(push(`/posts/date/${date}`)),
    filterByCategory: (category: Category) => dispatch(push(`/posts/category/${category.id}`)),
    filterByTag: (tag: Tag) => dispatch(push(`/posts/tag/${tag.id}`)),
    openLink: (link: Link) => dispatch(push(link.id)),
    changePage: (pageIndex: number, pageSize: number) => dispatch(push(`/page/${pageIndex + 1}`))
  })
)(PostsList)

// Home default screen
const Home = init(
  (store, match) => store.dispatch(homeInitAsync())
)(HomeConnected)

// Home paged
const HomePaged = init(
  (store, match) => store.dispatch(homeChangePageAsync(match.params.pageIndex - 1, 10))
)(HomeConnected)

// Home filter by date
const HomeFilterByDate = init(
  (store, match) => store.dispatch(homeFilterByDateAsync(match.params.date))
)(HomeConnected)

// Home filter by category
const HomeFilterByCategory = init(
  (store, match) => store.dispatch(homeFilterByCategoryAsync(match.params.categoryId))
)(HomeConnected)

// Home filter by tag
const HomeFilterByTag = init(
  (store, match) => store.dispatch(homeFilterByTagAsync(match.params.tagId))
)(HomeConnected)

export {
  Home,
  HomePaged,
  HomeFilterByDate,
  HomeFilterByCategory,
  HomeFilterByTag
}
