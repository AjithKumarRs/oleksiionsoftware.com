// React
import * as React from "react";
import styled from "theme";
import { connect, Store } from "react-redux";
import { match } from "react-router";
import { init } from "decorators";

// Custom UI
import {
  TopBar,
  Header,
  TopMenu,
  PostItem,
  DisqusResetCommentsCount
} from "components";

// Actions
import {
  push,
  homeInitAsync,
  homeChangePageAsync,
  homeFilterByTagAsync,
  homeFilterByDateAsync,
  homeFilterByCategoryAsync
} from "actions";

// Types
import { RootState } from "types";

// Semantic UI
import { Grid, Segment, Item, Sidebar, Menu } from "semantic-ui-react";

// Types
import { Post, Link, Category, Tag, Filter } from "types";

interface Props {
  className?: string;
  brand: string;
  links: Link[];
  filter: Filter;
  posts: Post[];
  pageIndex: number;
  pageSize: number;
  pagesCount: number;
}

interface DispatchProps {
  filterReset: () => void;
  filterByDate: (date: string) => void;
  filterByCategory: (category: Category) => void;
  filterByTag: (tag: Tag) => void;
  openLink: (link: Link) => void;
  openPost: (id: string) => void;
  changePage: (pageIndex: number, pageSize: number) => void;
}

export class PostsList extends React.Component<Props & DispatchProps> {
  handleFilterReset = () => this.props.filterReset();

  handleFilterByDate = (date: string) => this.props.filterByDate(date);

  handleFilterByCategory = (category: Category) =>
    this.props.filterByCategory(category);

  handleFilterByTag = (tag: Tag) => this.props.filterByTag(tag);

  handleLinkClick = (link: Link) => this.props.openLink(link);

  handlePostClick = (post: Post) => this.props.openPost(post.id);

  handlePageChange = (pageIndex: number, pageSize: number) =>
    this.props.changePage(pageIndex, pageSize);

  render() {
    return (
      <div className={this.props.className}>
        <TopBar
          brand={this.props.brand}
          links={this.props.links}
          onLinkClick={this.handleLinkClick}
        />

        <div className={"posts"}>
          {this.props.posts &&
            this.props.posts.map(pst => (
              <PostItem
                key={pst.id}
                mode={"short"}
                post={pst}
                onHeaderClick={this.handlePostClick}
                onDateClick={this.handleFilterByDate}
                onCategoryClick={this.handleFilterByCategory}
                onTagClick={this.handleFilterByTag}
              />
            ))}
        </div>
        <DisqusResetCommentsCount />
      </div>
    );
  }
}

// Styled Components
const PostsListStyled = styled(PostsList)`
  .posts {
    margin-top: 140px;
    margin-left: auto;
    margin-right: auto;
  }
`;

// Connected Components
const HomeConnected = connect(
  (state: RootState): Props => ({
    brand: state.home.brand,
    filter: state.home.filter,
    links: state.home.links,
    posts: state.home.posts,
    pagesCount: state.home.pagesCount,
    pageIndex: state.home.pageIndex,
    pageSize: state.home.pageSize
  }),
  (dispatch): DispatchProps => ({
    openPost: postId => dispatch(push(`/post/${postId}`)),
    filterReset: () => dispatch(push("/")),
    filterByDate: date => dispatch(push(`/posts/date/${date}`)),
    filterByCategory: category =>
      dispatch(push(`/posts/category/${category.id}`)),
    filterByTag: tag => dispatch(push(`/posts/tag/${tag.id}`)),
    openLink: link => dispatch(push(link.id)),
    changePage: (pageIndex, pageSize) =>
      dispatch(push(`/page/${pageIndex + 1}`))
  })
)(PostsListStyled);

// Home default screen
const Home = init((store: Store<RootState>, match: match<any>) =>
  store.dispatch<any>(homeInitAsync())
)(HomeConnected);

// Home paged
const HomePaged = init(
  (store: Store<RootState>, match: match<{ pageIndex: number }>) =>
    store.dispatch<any>(homeChangePageAsync(match.params.pageIndex - 1, 10))
)(HomeConnected);

// Home filter by date
const HomeFilterByDate = init(
  (store: Store<RootState>, match: match<{ date: string }>) =>
    store.dispatch<any>(homeFilterByDateAsync(match.params.date))
)(HomeConnected);

// Home filter by category
const HomeFilterByCategory = init(
  (store: Store<RootState>, match: match<{ categoryId: string }>) =>
    store.dispatch<any>(homeFilterByCategoryAsync(match.params.categoryId))
)(HomeConnected);

// Home filter by tag
const HomeFilterByTag = init(
  (store: Store<RootState>, match: match<{ tagId: string }>) =>
    store.dispatch<any>(homeFilterByTagAsync(match.params.tagId))
)(HomeConnected);

export {
  Home,
  HomePaged,
  HomeFilterByDate,
  HomeFilterByCategory,
  HomeFilterByTag
};
