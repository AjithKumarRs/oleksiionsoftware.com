// Framework
import * as React from "react";
import styled from "theme";
import { connect, Store, Dispatch } from "react-redux";

// Semantic UI
import { Menu, MenuItemProps } from "semantic-ui-react";

// Actions
import { push } from "actions";

// Types
import { RootState, Link } from "types";

interface TopMenuProps {
  className?: string;

  expanded: boolean;
  links: Link[];
}

interface TopMenuDispatchProps {
  onLinkClick: (link: Link) => void;
}

// Components
class TopMenu extends React.Component<TopMenuProps & TopMenuDispatchProps> {
  handleLinkClick = (e: React.SyntheticEvent<HTMLAnchorElement>, lnk: Link) => {
    e.preventDefault();

    this.props.onLinkClick(lnk);
  };

  render() {
    return (
      <div className={this.props.className}>
        {this.props.links &&
          this.props.links.map(lnk => (
            <a
              className={"menu-item"}
              key={lnk.id}
              onClick={e => this.handleLinkClick(e, lnk)}
              href={"https://oleksiionsoftware.com" + lnk.id}
            >
              {lnk.title}
            </a>
          ))}
      </div>
    );
  }
}

// Styled Components
const StyledTopMenu = styled(TopMenu)`
  display: ${(p: TopMenuProps) => (p.expanded ? "block" : "none")};
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 10px;
  box-shadow: 0 2px 2px -2px rgba(0, 0, 0, 0.15);
  text-align: center;

  .menu-item {
    display: block;
    font-family: Lato, "Helvetica Neue", Arial, Helvetica, sans-serif;
    font-weight: 400;
    font-style: normal;
    color: rgba(0, 0, 0, 0.54);
    text-transform: uppercase;
    font-size: 16px;
    line-height: 1.5;
    letter-spacing: 0.5px;
    word-spacing: 5px;
  }

  .menu-item:hover {
    color: rgba(0, 0, 0, 0.76);
  }

  @media (min-width: 768px) {
    width: 748px;
    display: block !important;

    .menu-item {
      display: inline-block;
      margin-left: 24px;
    }
  }

  @media (min-width: 1024px) {
    width: 1004px;
  }
`;

// Connected Components
const StyledTopMenuConnected = connect(
  (state: RootState): TopMenuProps => ({
    expanded: state.config.isMenuExpanded,
    links: state.config.links
  }),
  (dispatch: Dispatch<RootState>): TopMenuDispatchProps => ({
    onLinkClick: link => dispatch(push(link.id))
  })
)(StyledTopMenu);

export { StyledTopMenuConnected as TopMenu };
