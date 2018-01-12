// React
import * as React from "react";

// UI
import { Item } from "semantic-ui-react";

// Custom UI
import { PostDate, PostCategory, PostTag, DisqusComments } from "components";

// Types
import { Post, Category, Tag } from "types";

interface Props {
  post: Post;
  onDateClick: (date: string) => void;
  onCategoryClick: (category: Category) => void;
  onTagClick: (tag: Tag) => void;
}

declare var hljs: any;

// Components
export class LongPost extends React.Component<Props> {
  static defaultProps = {
    onDateClick: () => {},
    onCategoryClick: () => {},
    onTagClick: () => {}
  };

  componentDidMount() {
    hljs.initHighlighting.called = false;
    hljs.initHighlighting();
  }

  componentDidUpdate() {
    hljs.initHighlighting.called = false;
    hljs.initHighlighting();
  }

  handleDateClick = (date: string) => {
    this.props.onDateClick(date);
  };

  handleCategoryClick = (category: Category) => {
    this.props.onCategoryClick(category);
  };

  handleTagClick = (tag: Tag) => {
    this.props.onTagClick(tag);
  };

  render() {
    return (
      <Item key={this.props.post.id}>
        <Item.Content>
          <Item.Header as="a">{this.props.post.title}</Item.Header>
          {this.props.post.infobar && (
            <Item.Meta>
              {this.props.post.date && (
                <PostDate
                  onClick={this.handleDateClick}
                  date={this.props.post.date}
                />
              )}
              {this.props.post.category && (
                <PostCategory
                  onClick={this.handleCategoryClick}
                  category={this.props.post.category}
                />
              )}
              {this.props.post.tags &&
                this.props.post.tags.map(tag => (
                  <PostTag
                    key={tag.id}
                    onClick={this.handleTagClick}
                    tag={tag}
                  />
                ))}
            </Item.Meta>
          )}
          <Item.Description>
            <div>
              <div dangerouslySetInnerHTML={{ __html: this.props.post.body }} />
              <DisqusComments id={this.props.post.id} />
            </div>
          </Item.Description>
        </Item.Content>
      </Item>
    );
  }
}
