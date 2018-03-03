// React
import * as React from "react";
import styled from "theme";
import { connect, Store, Dispatch } from "react-redux";

// UI
import { Icon } from "semantic-ui-react";

// Custom UI
import { TopBarText, TopBarIcons } from "components";
import { RootState } from "types";

// Types
interface TopBrandProps {
  className?: string;
}

// Components
class TopBrand extends React.Component<TopBrandProps> {
  render() {
    return (
      <div className={this.props.className}>
        <TopBarText />
        <TopBarIcons />
      </div>
    );
  }
}

// Styled Components
const StyledTopBrand = styled(TopBrand)`
  text-align: center;
  margin-bottom: 5px !important;

  box-shadow: 0 2px 2px -2px rgba(0, 0, 0, 0.15);
  @media (min-width: 768px) {
    box-shadow: none;
  }
`;

export { StyledTopBrand as TopBrand };
