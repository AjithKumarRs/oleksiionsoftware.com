/* @flow */

// Framework
import React from 'react'

// UI
import {
  Menu,
  Icon
} from 'semantic-ui-react'

// Styles
import styles from './styles.css'

// Types
import type { Link } from 'types'

type Props = {
  brand: string,
  title: string,
  links: Link[],
  onLinkClick: (link: Link) => void
}

// Components
export class LeftNavigation extends React.Component<Props> {
  handleLinkClick = (lnk: Link) => {
    this.props.onLinkClick(lnk)
  }

  render () {
    return (
      <div className={styles.navigation}>
        <div className={styles.brand}>
          <Menu.Item className={styles.text}>
            {this.props.brand}
          </Menu.Item>
        </div>

        <div className={styles.links}>
          {
            this.props.links && this.props.links.map(lnk =>
              <Menu.Item key={lnk.id} name='cubes' className={styles.link} onClick={() => this.handleLinkClick(lnk)} link>
                {lnk.title}
              </Menu.Item>
            )
          }
        </div>

        <div className={styles.icons}>
          <a href='https://github.com/oleksii-udovychenko'><Icon name='github' size='large' inverted /></a>
          <a href='https://www.linkedin.com/in/oleksii-udovychenko'><Icon name='linkedin' size='large' inverted /></a>
          <a href='https://twitter.com/boades_net'><Icon name='twitter' size='large' inverted /></a>
        </div>

        <div className={styles.copyright}>
          <div className={styles['copyright-text']}>
            <div>
              Copyright <Icon name='copyright' className={styles['copyright-icon']} size='small' inverted />
            </div>
            <div>Oleksii Udovychenko</div>
            <div>2010 - 2017</div>
          </div>
        </div>
      </div>
    )
  }
}
