// Framework
import * as React from "react";
import styled from "theme";

// Custom UI
import { TopBrand, TopMenu, TopMenuHandler } from "components";

// Types
import { Link } from "types";

interface Props {
  className?: string;
}

// Components
class TopBar extends React.Component<Props> {
  render() {
    return (
      <div className={this.props.className}>
        <TopMenuHandler />
        <TopBrand />
        <TopMenu />
      </div>
    );
  }
}

// Styled Components
const TopBarStyled = styled(TopBar)`
  top: 0;
  width: 100%;
  background: white;
  z-index: 100;
  box-shadow: 0 2px 2px -2px rgba(0, 0, 0, 0.15);

  position: relative;
  @media (min-width: 768px) {
    position: fixed;
    box-shadow: none;
  }
`;

export { TopBarStyled as TopBar };
