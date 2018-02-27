// Framework
import * as React from "react";
import styled from "theme";

// Custom UI
import { TopBrand, TopMenu } from "components";

// Types
import { Link } from "types";

interface Props {
  className?: string;

  brand: string;
  links: Link[];

  onLinkClick: (link: Link) => void;
}

interface State {
  isToggled: boolean;
}

// Components
class TopBar extends React.Component<Props, State> {
  static defaultProps = {
    onLinkClick: () => {}
  };

  handleLinkClick = (link: Link) => this.props.onLinkClick(link);

  render() {
    return (
      <div className={this.props.className}>
        <TopBrand brand={this.props.brand} />
        <TopMenu links={this.props.links} onLinkClick={this.handleLinkClick} />
      </div>
    );
  }
}

// Styled Components
const TopBarStyled = styled(TopBar)`
  top: 0;
  width: 100%;
  position: fixed;
  background: white;
  box-shadow: 0 2px 2px -2px rgba(0, 0, 0, 0.15);
  z-index: 100;
`;
export { TopBarStyled as TopBar };
