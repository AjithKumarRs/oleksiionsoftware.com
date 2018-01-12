// Framework
import * as React from "react";

// UI
import { Button, Menu, Icon } from "semantic-ui-react";

// Types
interface Props {
  onToggle: (isToggled: boolean) => void;
}

interface State {
  isToggled: boolean;
}

// Components
export class TopBar extends React.Component<Props, State> {
  state = {
    isToggled: false
  };

  handleToggle = () => {
    this.props.onToggle(this.state.isToggled);
  };

  render() {
    return (
      <Menu fixed="top">
        <Menu.Item>
          <Button icon onClick={this.handleToggle}>
            <Icon name="sidebar" />
          </Button>
        </Menu.Item>

        {this.props.children}
      </Menu>
    );
  }
}
