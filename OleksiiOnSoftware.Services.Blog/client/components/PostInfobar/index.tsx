// Framework
import * as React from "react";
import styled from "theme";

// Custom UI
import { PostDate, PostCategory, PostTag, PostComments } from "components";

// Types
import { Tag, Category } from "types";

interface Props {
  className?: string;

  id: string;
  date: string;
  tags: Tag[];
  category: Category;
  comments: boolean;

  onDateClicked: (date: string) => void;
  onCategoryClicked: (category: Category) => void;
  onTagClicked: (tag: Tag) => void;
}

// Components
const PostInfobar = (props: Props) => (
  <div className={props.className}>
    {props.date && <PostDate onClick={props.onDateClicked} date={props.date} />}
    {props.category && <PostCategory onClick={props.onCategoryClicked} category={props.category} />}
    {props.tags && props.tags.map(tag => <PostTag key={tag.id} onClick={props.onTagClicked} tag={tag} />)}
    {props.comments && <PostComments url={props.id} />}
  </div>
);

// Styled Components
const StyledPostInfobar = styled(PostInfobar)`
  margin-top: 10px;
  margin-bottom: 10px;
  text-align: center;
  @media (min-width: 768px) {
    text-align: left;
  }

  .label {
    font-size: 14px !important;
    margin-bottom: 5px !important;
  }
`;

export { StyledPostInfobar as PostInfobar };
