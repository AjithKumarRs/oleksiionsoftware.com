// Framework
import * as React from "react";
import styled from "theme";

// Semantic UI
import { Menu, MenuItemProps } from "semantic-ui-react";

// Types
import { Link } from "types";

interface Props {
  className?: string;
  links: Link[];
  onLinkClick: (link: Link) => void;
}

// Components
class TopMenu extends React.Component<Props> {
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
              onClick={(e) => this.handleLinkClick(e, lnk)}
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
  margin-left: auto;
  margin-right: auto;
  margin-bottom: 10px;
  width: 300px;
  text-align: center;

  .menu-item {
    font-family: Lato, "Helvetica Neue", Arial, Helvetica, sans-serif;
    font-weight: 400;
    font-style: normal;
    color: rgba(0, 0, 0, 0.54);
    margin-left: 24px;
    text-transform: uppercase;
    font-size: 16px;
    line-height: 1.5;
    letter-spacing: 0.5px;
    word-spacing: 5px;
  }

  .menu-item:hover {
    color: rgba(0, 0, 0, 0.76);
  }

  @media (min-width: 320px) {
    img {
      max-width: 300px;
    }
  }

  @media (min-width: 375px) {
    width: 355px;

    img {
      max-width: 355px;
    }
  }

  @media (min-width: 425px) {
    width: 405px;

    img {
      max-width: 405px;
    }
  }

  @media (min-width: 768px) {
    width: 748px;

    img {
      max-width: 748px;
    }
  }

  @media (min-width: 1024px) {
    width: 1004px;

    img {
      max-width: 1004px;
    }
  }
`;

export { StyledTopMenu as TopMenu };
