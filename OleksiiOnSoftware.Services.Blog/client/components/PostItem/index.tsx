// React
import * as React from "react";
import styled from "theme";

// UI
import { Item } from "semantic-ui-react";

// Custom UI
import {
  PostHeader,
  PostDate,
  PostCategory,
  PostTag,
  PostInfobar,
  DisqusComments,
  PostContent
} from "components";

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
  className?: string;

  mode: string;
  post: Post;

  onHeaderClick?: (post: Post) => void;
  onDateClick?: (date: string) => void;
  onCategoryClick?: (category: Category) => void;
  onTagClick?: (tag: Tag) => void;
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
class PostItem extends React.Component<Props> {
  static defaultProps = {
    onHeaderClick: () => {},
    onDateClick: () => {},
    onCategoryClick: () => {},
    onTagClick: () => {}
  };

  componentDidMount() {
    this.props.mode == "long" && highlightAll();
  }

  componentDidUpdate() {
    this.props.mode == "long" && highlightAll();
  }

  handleHeaderClick = (post: Post) => {
    this.props.onHeaderClick(post);
  };

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
    const isShowCommentsCount = this.props.mode === "short";
    const isShowCommentsSection = this.props.mode === "long";
    const isClickable = this.props.mode === "short";
    const content =
      this.props.mode === "short"
        ? this.props.post.short
        : this.props.post.body;

    return (
      <div className={this.props.className} key={this.props.post.id}>
        <PostHeader
          id={this.props.post.id}
          clickable={isClickable}
          onClick={() => this.handleHeaderClick(this.props.post)}
        >
          {this.props.post.title}
        </PostHeader>

        <PostInfobar
          id={this.props.post.id}
          date={this.props.post.date}
          onDateClicked={this.handleDateClick}
          category={this.props.post.category}
          onCategoryClicked={this.props.onCategoryClick}
          tags={this.props.post.tags}
          onTagClicked={this.handleTagClick}
          comments={isShowCommentsCount}
        />

        <PostContent content={content} />
        {isShowCommentsSection && <DisqusComments id={this.props.post.id} />}
      </div>
    );
  }
}

// Styled Components
const StyledPostItem = styled(PostItem)`
  margin-left: auto;
  margin-right: auto;

  width: 300px;
  img {
    max-width: 300px;
  }

  @media (min-width: 320px) {
    width: 300px;

    img {
      max-width: 300px;
    }
  }

  @media (min-width: 375px) {
    width: 355px;

    img {
      max-width: 355px;
    }
  }

  @media (min-width: 425px) {
    width: 405px;

    img {
      max-width: 405px;
    }
  }

  @media (min-width: 768px) {
    width: 748px;

    img {
      max-width: 748px;
    }
  }

  @media (min-width: 1024px) {
    width: 1004px;

    img {
      max-width: 1004px;
    }
  }
`;

export { StyledPostItem as PostItem };
