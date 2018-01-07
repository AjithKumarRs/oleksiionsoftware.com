/* @flow */

// React
import React from 'react'

// Custom UI
import {
  TopBar,
  Header,
  ShortPost,
  Pagination,
  LeftNavigation,
  DisqusResetCommentsCount
} from 'components'

// Semantic UI
import {
  Grid,
  Segment,
  Item,
  Sidebar,
  Menu
} from 'semantic-ui-react'

// Types
import type { Post, Filter, Category, Tag, Link } from 'types'

// Styles
const style = {
  appBar: {
    width: '100%'
  },

  mainContent: {
    height: '100%'
  },

  topSegment: {
    margin: 0,
    padding: 0
  },

  posts: {
    marginTop: '40px'
  }
}

type Props = {
  isSidebarOpen: boolean,
  brand: string,
  pageIndex: number,
  pageSize: number,
  pagesCount: number,
  postsCount: number,
  links: Link[],
  posts: Post[],
  filter: Filter,

  toggleSidebar: () => void,
  filterReset: () => void,
  filterByDate: (date: string) => void,
  filterByCategory: (category: Category) => void,
  filterByTag: (tag: Tag) => void,
  openLink: (link: Link) => void,
  openPost: (postId: string) => void,
  changePage: (pageIndex: number, pageSize: number) => void
}

export class PostsList extends React.Component<Props> {
  handleSidebarToggle = () => this.props.toggleSidebar()

  handleFilterReset = () => this.props.filterReset()

  handleFilterByDate = (date: string) => this.props.filterByDate(date)

  handleFilterByCategory = (category: Category) => this.props.filterByCategory(category)

  handleFilterByTag = (tag: Tag) => this.props.filterByTag(tag)

  handleLinkClick = (link: Link) => this.props.openLink(link)

  handlePostClick = (post: Post) => this.props.openPost(post.id)

  handlePageChange = (pageIndex: number, pageSize: number) => this.props.changePage(pageIndex, pageSize)

  render () {
    return (
      <div className={'main-content'} style={style.mainContent}>
        <Sidebar as={Menu} width='thin' visible={this.props.isSidebarOpen} icon='labeled' vertical inverted>
          <LeftNavigation
            brand={this.props.brand}
            links={this.props.links}
            onLinkClick={this.handleLinkClick} />
        </Sidebar>

        <TopBar onToggle={this.handleSidebarToggle}>
          <Menu.Item className={'borderless'}>
            <Header
              filter={this.props.filter}
              onHomeClick={this.handleFilterReset} />
          </Menu.Item>
        </TopBar>

        <Sidebar.Pusher>
          <Grid columns={1} stretched stackable style={style.posts}>
            <Grid.Row>
              <Grid.Column stretched>
                <Segment basic style={style.topSegment}>
                  <Item.Group>
                    <div className={'top-header'}>
                      {this.props.pagesCount > 1 &&
                        <Pagination
                          pageIndex={this.props.pageIndex}
                          pageSize={this.props.pageSize}
                          pagesCount={this.props.pagesCount}
                          onPageChanged={this.handlePageChange} />
                      }
                    </div>
                  </Item.Group>
                </Segment>

                <Segment basic>
                  <Item.Group>
                    {
                      this.props.posts && this.props.posts.map(pst =>
                        <ShortPost
                          key={pst.id}
                          post={pst}
                          onHeaderClick={this.handlePostClick}
                          onDateClick={this.handleFilterByDate}
                          onCategoryClick={this.handleFilterByCategory}
                          onTagClick={this.handleFilterByTag} />)
                    }
                  </Item.Group>
                </Segment>
              </Grid.Column>
            </Grid.Row>
          </Grid>
        </Sidebar.Pusher>

        <DisqusResetCommentsCount />
      </div>
    )
  }
}
