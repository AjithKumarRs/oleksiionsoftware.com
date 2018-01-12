// Framework
import * as React from "react";
import { connect, Store } from "react-redux";
import { match } from "react-router";
import { init } from "decorators";

// Custom UI
import { LongPost, LeftNavigation, TopBar } from "components";

// Semantic UI
import {
  Grid,
  Segment,
  Item,
  Icon,
  Sidebar,
  Menu,
  Breadcrumb
} from "semantic-ui-react";

// Types
import {
  RootState,
  Post as PostType,
  Link,
  Category,
  Tag,
  HomeInitActions,
  PostInitActions
} from "types";

// Actions
import { toggleSidebar, postInitAsync, push } from "actions";
import { Dispatch } from "redux";

interface Props {
  isSidebarOpen: boolean;
  brand: string;
  links: Link[];
  post: PostType;
}

interface DispatchProps {
  toggleSidebar: () => void;
  filterByDate: (date: string) => void;
  filterByCategory: (category: Category) => void;
  filterByTag: (tag: Tag) => void;
  openLink: (link: Link) => void;
  goHome: () => void;
}

export class PostComponent extends React.Component<Props & DispatchProps> {
  handleSidebarToggle = () => this.props.toggleSidebar();

  handleFilterByDate = (date: string) => this.props.filterByDate(date);

  handleFilterByCategory = (category: Category) =>
    this.props.filterByCategory(category);

  handleFilterByTag = (tag: Tag) => this.props.filterByTag(tag);

  handleLinkClick = (link: Link) => this.props.openLink(link);

  handleHomeClick = () => this.props.goHome();

  render() {
    const homeLink = (
      <div onClick={this.handleHomeClick}>
        <Icon name="arrow left" /> Home
      </div>
    );

    return (
      <div className={"main-content"}>
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
            <Breadcrumb
              icon="right angle"
              sections={[
                { key: "home", content: homeLink, link: true, active: true }
              ]}
            />
          </Menu.Item>
        </TopBar>

        <Sidebar.Pusher>
          <Grid columns={1} stretched stackable style={{ marginTop: "40px" }}>
            <Grid.Row>
              <Grid.Column stretched>
                <Segment basic>
                  <Item.Group>
                    <LongPost
                      post={this.props.post}
                      onDateClick={this.handleFilterByDate}
                      onCategoryClick={this.handleFilterByCategory}
                      onTagClick={this.handleFilterByTag}
                    />
                  </Item.Group>
                </Segment>
              </Grid.Column>
            </Grid.Row>
          </Grid>
        </Sidebar.Pusher>
      </div>
    );
  }
}

const PostConnected = connect(
  (state: RootState): Props => ({
    isSidebarOpen: state.config.isSidebarOpen,
    brand: state.post.brand,
    links: state.post.links,
    post: state.post as PostType
  }),
  (dispatch: Dispatch<RootState>): DispatchProps => ({
    toggleSidebar: () => dispatch(toggleSidebar()),
    goHome: () => dispatch(push(`/`)),
    openLink: link => dispatch(push(link.id)),
    filterByDate: date => dispatch(push(`/posts/date/${date}`)),
    filterByCategory: category =>
      dispatch(push(`/posts/category/${category.id}`)),
    filterByTag: tag => dispatch(push(`/posts/tag/${tag.id}`))
  })
)(PostComponent);

const PostInited = init(
  (store: Store<RootState>, match: match<{ postId: string }>) =>
    store.dispatch<any>(postInitAsync(match.params.postId))
)(PostConnected);

export { PostInited as Post };
