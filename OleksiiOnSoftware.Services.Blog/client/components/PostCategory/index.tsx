// Framework
import * as React from "react";

// UI
import { Icon, Label } from "semantic-ui-react";

// Types
import { Category } from "types";

interface Props {
  category: Category;

  onClick: (category: Category) => void;
}

// Components
export class PostCategory extends React.Component<Props> {
  static defaultProps = {
    onClick: () => {}
  };

  handleClick = () => {
    this.props.onClick(this.props.category);
  };

  render() {
    return (
      <Label as="a" onClick={this.handleClick}>
        <Icon name="folder" /> {this.props.category.title}
      </Label>
    );
  }
}
