// Framework
import * as React from "react";
import styled from "theme";
import { connect, Store, Dispatch } from "react-redux";

// UI
import { Icon } from "semantic-ui-react";

// Actions
import { toggleMenu } from "actions";

// Types
import { RootState } from "types";

interface TopMenuHandlerProps {
  className?: string;

  isMenuExpanded: boolean;
}

interface TopMenuHandlerDispatchProps {
  onToggleMenu?: () => void;
}

// Components
class TopMenuHandler extends React.Component<TopMenuHandlerProps & TopMenuHandlerDispatchProps> {
  static defaultProps = {
    onToggleMenu: () => {}
  };

  handleClick = (e: React.SyntheticEvent<HTMLAnchorElement>) => {
    e.preventDefault();

    this.props.onToggleMenu();
  };

  render() {
    return (
      <a href="#" className={this.props.className} onClick={this.handleClick}>
        <Icon className={"handle"} name={this.props.isMenuExpanded ? "close" : "content"} size="large" />
      </a>
    );
  }
}

// Styled Components
const StyledTopMenuHandler = styled(TopMenuHandler)`
  display: block;
  position: absolute;
  top: 4px;
  left: 4px;
  zoom: 1.3;
  color: #e8e8e8 !important;

  &:hover {
    color: rgba(0, 0, 0, 0.84) !important;
    transform: scale(1.3);
    cursor: pointer;
  }

  @media (min-width: 768px) {
    display: none;
  }
`;

// Connected Components
const StyledTopMenuHandlerConnected = connect(
  (state: RootState): TopMenuHandlerProps => ({
    isMenuExpanded: state.config.isMenuExpanded
  }),
  (dispatch: Dispatch<RootState>): TopMenuHandlerDispatchProps => ({
    onToggleMenu: () => dispatch(toggleMenu())
  })
)(StyledTopMenuHandler);

export { StyledTopMenuHandlerConnected as TopMenuHandler };
