// Framework
import * as React from "react";

// UI
import { Icon, Label } from "semantic-ui-react";

// Types
interface Props {
  date: string;

  onClick: (date: string) => void;
}

// Components
export class PostDate extends React.Component<Props> {
  static defaultProps = {
    onClick: () => {}
  };

  handleClick = () => {
    this.props.onClick(this.props.date);
  };

  render() {
    return (
      <Label as="a" onClick={this.handleClick}>
        <Icon name="calendar" /> &nbsp; {this.props.date}
      </Label>
    );
  }
}
