// Framework
import * as React from "react";
import styled from "theme";
import { Dispatch } from "redux";
import { connect, Store } from "react-redux";
import { match } from "react-router";
import { init } from "decorators";

// Custom UI
import { PostItem, TopBar } from "components";

// Semantic UI
import { Grid, Segment, Item, Icon, Sidebar, Menu, Breadcrumb, Header } from "semantic-ui-react";

// Types
import { RootState, Post as PostType, Link, Category, Tag, HomeInitActions, PostInitActions } from "types";

// Actions
import { postInitAsync, push } from "actions";

// Types
interface Props {
  className?: string;

  post: PostType;
}

interface DispatchProps {
  filterByDate: (date: string) => void;
  filterByCategory: (category: Category) => void;
  filterByTag: (tag: Tag) => void;
  openLink: (link: Link) => void;
}

// Components
class PostComponent extends React.Component<Props & DispatchProps> {
  handleFilterByDate = (date: string) => this.props.filterByDate(date);

  handleFilterByCategory = (category: Category) => this.props.filterByCategory(category);

  handleFilterByTag = (tag: Tag) => this.props.filterByTag(tag);

  handleLinkClick = (link: Link) => this.props.openLink(link);

  render() {
    return (
      <div className={this.props.className}>
        <TopBar />

        <PostItem
          className={"post"}
          mode={"long"}
          post={this.props.post}
          onDateClick={this.handleFilterByDate}
          onCategoryClick={this.handleFilterByCategory}
          onTagClick={this.handleFilterByTag}
        />
      </div>
    );
  }
}

// Styled Components
const PostStyled = styled(PostComponent)`
  .post {
    margin-top: 0px;
    margin-left: auto;
    margin-right: auto;
  }

  @media (min-width: 768px) {
    .post {
      margin-top: 140px;
    }
  }
`;

// Connected Components
const PostConnected = connect(
  (state: RootState): Props => ({
    post: state.post as PostType
  }),
  (dispatch: Dispatch<RootState>): DispatchProps => ({
    openLink: link => dispatch(push(link.id)),
    filterByDate: date => dispatch(push(`/posts/date/${date}`)),
    filterByCategory: category => dispatch(push(`/posts/category/${category.id}`)),
    filterByTag: tag => dispatch(push(`/posts/tag/${tag.id}`))
  })
)(PostStyled);

const PostInited = init((store: Store<RootState>, match: match<{ postId: string }>) =>
  store.dispatch<any>(postInitAsync(match.params.postId))
)(PostConnected);

export { PostInited as Post };
