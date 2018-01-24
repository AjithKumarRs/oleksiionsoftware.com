// React
import * as React from "react";
import { connect, Store } from "react-redux";
import { match } from "react-router";
import { init } from "decorators";

// Custom UI
import {
  TopBar,
  Header,
  ShortPost,
  Pagination,
  LeftNavigation,
  DisqusResetCommentsCount
} from "components";

// Actions
import {
  push,
  toggleSidebar,
  homeInitAsync,
  homeFilterByCategoryAsync,
  homeFilterByTagAsync,
  homeFilterByDateAsync,
  homeChangePageAsync
} from "actions";

// Types
import { RootState } from "types";

// Semantic UI
import { Grid, Segment, Item, Sidebar, Menu } from "semantic-ui-react";

// Styles
const style = {
  appBar: {
    width: "100%"
  },

  mainContent: {
    height: "100%"
  },

  topSegment: {
    margin: 0,
    padding: 0
  },

  posts: {
    marginTop: "40px"
  }
};

// Types
import { Post, Link, Category, Tag, Filter } from "types";

interface Props {
  isSidebarOpen: boolean;
  brand: string;
  links: Link[];
  filter: Filter;
  posts: Post[];
  pageIndex: number;
  pageSize: number;
  pagesCount: number;
}

interface DispatchProps {
  toggleSidebar: () => void;
  filterReset: () => void;
  filterByDate: (date: string) => void;
  filterByCategory: (category: Category) => void;
  filterByTag: (tag: Tag) => void;
  openLink: (link: Link) => void;
  openPost: (id: string) => void;
  changePage: (pageIndex: number, pageSize: number) => void;
}

export class PostsList extends React.Component<Props & DispatchProps> {
  handleSidebarToggle = () => this.props.toggleSidebar();

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
    const postsStyle = { ...style.posts, marginRight: "10px" };
    if (this.props.isSidebarOpen) {
      postsStyle.marginRight = "150px";
    }

    return (
      <div className={"main-content"} style={style.mainContent}>
        <Sidebar
          as={Menu}
          width="thin"
          visible={this.props.isSidebarOpen}
          icon="labeled"
          vertical
          inverted
        >
          <LeftNavigation
            brand={this.props.brand}
            links={this.props.links}
            onLinkClick={this.handleLinkClick}
          />
        </Sidebar>

        <TopBar onToggle={this.handleSidebarToggle}>
          <Menu.Item className={"borderless"}>
            <Header
              filter={this.props.filter}
              onHomeClick={this.handleFilterReset}
            />
          </Menu.Item>
        </TopBar>

        <Sidebar.Pusher>
          <Grid columns={1} stretched stackable style={postsStyle}>
            <Grid.Row>
              <Grid.Column stretched>
                <Segment basic style={style.topSegment}>
                  <Item.Group>
                    <div className={"top-header"}>
                      {this.props.pagesCount > 1 && (
                        <Pagination
                          pageIndex={this.props.pageIndex}
                          pageSize={this.props.pageSize}
                          pagesCount={this.props.pagesCount}
                          onPageChanged={this.handlePageChange}
                        />
                      )}
                    </div>
                  </Item.Group>
                </Segment>

                <Segment basic>
                  <Item.Group>
                    {this.props.posts &&
                      this.props.posts.map(pst => (
                        <ShortPost
                          key={pst.id}
                          post={pst}
                          onHeaderClick={this.handlePostClick}
                          onDateClick={this.handleFilterByDate}
                          onCategoryClick={this.handleFilterByCategory}
                          onTagClick={this.handleFilterByTag}
                        />
                      ))}
                  </Item.Group>
                </Segment>
              </Grid.Column>
            </Grid.Row>
          </Grid>
        </Sidebar.Pusher>

        <DisqusResetCommentsCount />
      </div>
    );
  }
}

// Connect PostsList to Redux store
const HomeConnected = connect(
  (state: RootState): Props => ({
    isSidebarOpen: state.config.isSidebarOpen,
    brand: state.home.brand,
    filter: state.home.filter,
    links: state.home.links,
    posts: state.home.posts,
    pagesCount: state.home.pagesCount,
    pageIndex: state.home.pageIndex,
    pageSize: state.home.pageSize
  }),
  (dispatch): DispatchProps => ({
    toggleSidebar: () => dispatch(toggleSidebar()),
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
)(PostsList);

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
