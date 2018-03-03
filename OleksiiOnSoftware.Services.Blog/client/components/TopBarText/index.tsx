// Framework
import * as React from "react";
import styled from "theme";
import { connect, Store, Dispatch } from "react-redux";

// Types
import { RootState } from "types";

interface TopBarTextProps {
  className?: string;
  brand: string;
}

// Components
const TopBarText = ({ className, brand }: TopBarTextProps) => <h2 className={className}>{brand}</h2>;

// Styled Components
const StyledTopBarText = styled(TopBarText)`
  margin: 0;
  padding: 0;
  font-family: Lato, "Helvetica Neue", Arial, Helvetica, sans-serif;
  font-weight: 600;
  font-style: normal;
  color: rgba(0, 0, 0, 0.84);
  display: block;
  line-height: 1.5;
  letter-spacing: 0.5;
  word-spacing: 5;
  margin-left: auto;
  margin-right: auto;
  text-align: center;

  @media (min-width: 768px) {
    font-size: 30px;
  }
`;

// Connected Components
const StyledTopBarTextConnected = connect((state: RootState): TopBarTextProps => ({
  brand: state.config.brand
}))(StyledTopBarText);

export { StyledTopBarTextConnected as TopBarText };
