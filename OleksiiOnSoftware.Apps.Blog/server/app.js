import express from 'express'
import proxy from 'express-http-proxy'
import url from 'url'

import { blogs, posts, links } from 'utils'

// Specialized API for blog UI
const app = express.Router()

app.use('/api', proxy('http://api:5001', {
  proxyReqPathResolver: req => '/api' + url.parse(req.url).path
}));

// TODO: Implement HATEOAS

// Expose minimal API required by Front-End
app.get('/api/blogs', (req, res) => {
  // TODO: Implement paging
  // TODO: Wireup with backend
  res.json({
    items: blogs
  })
})

// TODO: Test with empty arrays
// TODO: Map results from back-end to front-end models
app.get('/api/blogs/:blogId', (req, res) => {
  let filteredPosts = posts
  let filter = {
    by: 'none'
  }

  // Filter all posts by category
  if (req.query.filterByCategory) {
    filter = {
      by: 'category',
      title: req.query.filterByCategory
    }

    filteredPosts = filteredPosts.filter((item) => {
      return item.category.id === req.query.filterByCategory
    })
  }

  // Filter all posts by tag
  if (req.query.filterByTag) {
    filter = {
      by: 'tag',
      title: req.query.filterByTag
    }

    filteredPosts = filteredPosts.filter(item => {
      return item.tags.find(tag => {
        return tag.id === req.query.filterByTag
      })
    })
  }

  // Filter all posts by date
  if (req.query.filterByDate) {
    filter = {
      by: 'date',
      title: req.query.filterByDate
    }

    filteredPosts = filteredPosts.filter((item) => {
      return new Date(item.date).getTime() === new Date(req.query.filterByDate).getTime()
    })
  }

  // Paging
  let pageSize = parseInt(req.query.pageSize) || 10
  let pageIndex = parseInt(req.query.pageIndex) || 0

  let startIndex = pageIndex * pageSize
  let endIndex = startIndex + pageSize

  if (endIndex > filteredPosts.length) {
    endIndex = filteredPosts.length
  }

  let postsToSend = filteredPosts.slice(startIndex, endIndex)

  res.json({
    id: req.params.blogId,
    brand: 'Oleksii Udovychenko',
    copyright: '2015-2017 Oleksii Udovychenko (C)',
    filter: filter,
    links: links,
    posts: postsToSend,
    postsCount: filteredPosts.length,
    pageIndex: pageIndex,
    pageSize: pageSize,
    pagesCount: filteredPosts.length / pageSize
  })
})

app.get('/api/blogs/:blogId/posts/:postId', (req, res) => {
  const postId = req.params.postId

  const filteredPosts = posts.filter(item => {
    return item.key === postId
  })

  res.json({
    id: req.params.blogId,
    brand: 'Oleksii Udovychenko',
    copyright: '2015-2017 Oleksii Udovychenko (C)',
    links: links,
    posts: filteredPosts
  })
})

module.exports = app
