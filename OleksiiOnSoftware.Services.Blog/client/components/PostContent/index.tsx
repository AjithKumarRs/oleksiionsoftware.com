// Framework
import * as React from "react";
import styled from "theme";

// Types
interface Props {
  className?: string;
  content: string;
}

// Components
const PostContent = ({ className, content }: Props) => (
  <div className={className} dangerouslySetInnerHTML={{ __html: content }} />
);

// Styled Components
const PostContentStyled = styled(PostContent)`
  margin-top: 20px;
  margin-bottom: 50px;
  text-align: justify;

  h4 {
    font-family: Lato, "Helvetica Neue", Arial, Helvetica, sans-serif;
    font-weight: 600;
    font-style: normal;
    font-size: 24px;
    line-height: 1.5;
    color: rgba(0, 0, 0, 0.84);
    display: block;
  }

  ul {
    margin-top: 0px;
    padding-left: 20px;
  }

  p,
  li {
    font-family: Lato, "Helvetica Neue", Arial, Helvetica, sans-serif;
    font-weight: 400;
    font-style: normal;
    font-size: 20px;
    line-height: 1.5;
    letter-spacing: 0.5px;
    word-spacing: 5px;
    margin: 0px 0px 1em;
  }

  li {
    margin-bottom: 0px;
  }
`;

export { PostContentStyled as PostContent };
