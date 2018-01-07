/* @flow */

// Framework
import React from 'react'

// Custom UI
import {
  LongPost,
  LeftNavigation,
  TopBar
} from 'components'

// Semantic UI
import {
  Grid,
  Segment,
  Item,
  Icon,
  Sidebar,
  Menu,
  Breadcrumb
} from 'semantic-ui-react'

// Types
import type { Tag, Category, Post as PostType, Link } from 'types'

type Props = {
  isSidebarOpen: boolean,
  brand: string,
  links: Link[],
  post: PostType,

  goHome: () => void,
  toggleSidebar: () => void,
  filterByDate: (date: string) => void,
  filterByCategory: (category: Category) => void,
  filterByTag: (tag: Tag) => void
}

export class Post extends React.Component<Props> {
  handleSidebarToggle = () => this.props.toggleSidebar()

  handleFilterByDate = (date: string) => this.props.filterByDate(date)

  handleFilterByCategory = (category: Category) => this.props.filterByCategory(category)

  handleFilterByTag = (tag: Tag) => this.props.filterByTag(tag)

  handleLinkClick = (link: Link) => console.log(link)

  handleHomeClick = () => this.props.goHome()

  render () {
    const homeLink = (
      <div onClick={this.handleHomeClick} >
        <Icon name='arrow left' /> Home
      </div>
    )

    return (
      <div className={'main-content'}>
        <Sidebar as={Menu} width='thin' visible={this.props.isSidebarOpen} icon='labeled' vertical inverted>
          <LeftNavigation
            brand={this.props.brand}
            links={this.props.links}
            onLinkClick={this.handleLinkClick} />
        </Sidebar>

        <TopBar onToggle={this.handleSidebarToggle}>
          <Menu.Item className={'borderless'}>
            <Breadcrumb icon='right angle' sections={[
              { key: 'home', content: homeLink, link: true, active: true }
            ]} />
          </Menu.Item>
        </TopBar>

        <Sidebar.Pusher>
          <Grid columns={1} stretched stackable style={{ marginTop: '40px' }}>
            <Grid.Row>
              <Grid.Column stretched>
                <Segment basic>
                  <Item.Group>
                    <LongPost
                      post={this.props.post}
                      onDateClick={this.handleFilterByDate}
                      onCategoryClick={this.handleFilterByCategory}
                      onTagClick={this.handleFilterByTag} />
                  </Item.Group>
                </Segment>
              </Grid.Column>
            </Grid.Row>
          </Grid>
        </Sidebar.Pusher>
      </div>
    )
  }
}
