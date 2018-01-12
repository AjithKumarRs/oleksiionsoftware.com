// Framework
import * as React from "react";

// UI
import { Icon, Label } from "semantic-ui-react";

// Types
import { Tag } from "types";

interface Props {
  tag: Tag;
  onClick: (tag: Tag) => void;
}

// Components
export class PostTag extends React.Component<Props> {
  static defaultProps = {
    onClick: () => {}
  };

  handleClick = () => {
    this.props.onClick(this.props.tag);
  };

  render() {
    return (
      <Label as="a" onClick={this.handleClick}>
        <Icon name="tag" /> &nbsp; {this.props.tag.title}
      </Label>
    );
  }
}
