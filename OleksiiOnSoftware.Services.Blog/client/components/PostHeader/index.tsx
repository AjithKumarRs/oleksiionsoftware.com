import * as React from "react";

import styled from "theme";
import { ReactNode } from "react";

interface Props {
  id: string;
  clickable: boolean;
  onClick: () => void;

  children?: React.ReactNode;
}

const PostHeaderH2 = styled.h2`
  font-family: Lato, "Helvetica Neue", Arial, Helvetica, sans-serif;
  font-weight: 600;
  font-style: normal;
  font-size: 30px;
  color: rgba(0, 0, 0, 0.84);
  display: block;
  padding-bottom: 5px;
  text-align: justify;
  line-height: 1.5;
  letter-spacing: 0.5;
  word-spacing: 5;
`;

const PostHeaderA = styled.a`
  font-family: Lato, "Helvetica Neue", Arial, Helvetica, sans-serif;
  font-weight: 600;
  font-style: normal;
  font-size: 30px;
  color: rgba(0, 0, 0, 0.84);
  display: block;
  padding-bottom: 0px;
  text-align: justify;
  line-height: 1.5;
  letter-spacing: 0.5;
  word-spacing: 5;
  cursor: pointer;

  &:hover {
    color: rgba(0, 0, 0, 0.84);
    text-decoration: underline;
  }
`;

class PostHeader extends React.Component<Props> {
  static defaultProps = {
    onClick: () => {}
  };

  handleHeaderClick = (e: React.SyntheticEvent<HTMLAnchorElement>) => {
    e.preventDefault();

    this.props.onClick();
  };

  render() {
    if (this.props.clickable) {
      return (
        <PostHeaderA
          href={"https://oleksiionsoftware.com/post/" + this.props.id}
          onClick={this.handleHeaderClick}
        >
          {this.props.children}
        </PostHeaderA>
      );
    }

    return <PostHeaderH2>{this.props.children}</PostHeaderH2>;
  }
}

export { PostHeader };
