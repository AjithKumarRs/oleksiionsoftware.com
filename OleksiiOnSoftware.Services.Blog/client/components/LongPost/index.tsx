// React
import * as React from "react";

// UI
import { Item } from "semantic-ui-react";

// Custom UI
import { PostDate, PostCategory, PostTag, DisqusComments } from "components";

// Types
import { Post, Category, Tag } from "types";

import "prismjs";
import "prismjs/components/prism-css";
import "prismjs/components/prism-javascript";
import "prismjs/components/prism-java";
import "prismjs/components/prism-markup";
import "prismjs/components/prism-typescript";
import "prismjs/components/prism-sass";
import "prismjs/components/prism-scss";
import "prismjs/components/prism-yaml";
import "prismjs/components/prism-bash";
import "prismjs/components/prism-powershell";
import "prismjs/components/prism-csharp";
import "prismjs/components/prism-asm6502";
import "prismjs/components/prism-jsx";

import "prismjs/plugins/command-line/prism-command-line";
import "prismjs/plugins/line-numbers/prism-line-numbers";
import "prismjs/plugins/normalize-whitespace/prism-normalize-whitespace";

declare var Prism: any;

interface Props {
  post: Post;
  onDateClick: (date: string) => void;
  onCategoryClick: (category: Category) => void;
  onTagClick: (tag: Tag) => void;
}

const highlightAll = () => {
  // Fix DOM for command-line plugin
  const cmdCodeTags = document.querySelectorAll(".command-line");
  const cmdAttributes = [
    "data-prompt",
    "data-output",
    "data-user",
    "data-host"
  ];

  for (let i = 0; i < cmdCodeTags.length; i++) {
    // Read attributes for command-line plugin from 'code' node
    const codeTag = cmdCodeTags[i];
    const preTag = codeTag.parentElement;
    for (const attrName of cmdAttributes) {
      if (codeTag.hasAttribute(attrName)) {
        const attrValue = codeTag.getAttribute(attrName);
        preTag.setAttribute(attrName, attrValue);
        codeTag.removeAttribute(attrName);
      }
    }
  }

  Prism.highlightAll();
};

// Components
export class LongPost extends React.Component<Props> {
  static defaultProps = {
    onDateClick: () => {},
    onCategoryClick: () => {},
    onTagClick: () => {}
  };

  componentDidMount() {
    highlightAll();
  }

  componentDidUpdate() {
    highlightAll();
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
        <Item.Content style={{ width: "100%" }}>
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
              <div
                style={{ textAlign: "justify" }}
                dangerouslySetInnerHTML={{ __html: this.props.post.body }}
              />
              <DisqusComments id={this.props.post.id} />
            </div>
          </Item.Description>
        </Item.Content>
      </Item>
    );
  }
}
