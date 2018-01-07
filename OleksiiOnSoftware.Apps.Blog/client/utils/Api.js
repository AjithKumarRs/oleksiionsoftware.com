/* globals URLSearchParams */
import fetch from 'isomorphic-fetch'

class UrlBuilder {
  params = {
    baseUrl: '',
    path: [],
    query: {}
  }

  static fromUrl (baseUrl) {
    return new UrlBuilder(baseUrl)
  }

  static fromRoot () {
    return new UrlBuilder(window.location.origin)
  }

  constructor (baseUrl) {
    this.params.baseUrl = baseUrl
  }

  addPath (segment) {
    this.params.path.push(segment)
    return this
  }

  setParam (name, value) {
    this.params.query[name] = value
    return this
  }

  setParams (obj) {
    for (const key of Object.keys(obj)) {
      this.setParam(key, obj[key])
    }

    return this
  }

  async fetch () {
    const req = await fetch(this.toString())
    if (req.status >= 400) {
      throw Error('Request failed')
    }

    const json = await req.json()
    return json
  }

  toString () {
    // Start with the base url
    let url = this.params.baseUrl

    // Add path
    let path = '/'
    if (this.params.path.length > 0) {
      path = '/' + this.params.path.join('/') + '/'
    }

    url += path
    url = url.trim()

    // Add query string part, if any
    if (Object.keys(this.params.query).length > 0) {
      let searchParams = new URLSearchParams()
      for (let param in this.params.query) {
        searchParams.append(param, this.params.query[param])
      }

      url += '?' + searchParams.toString()
    }

    return url
  }
}

const api = (): UrlBuilder => {
  return UrlBuilder
}

export { api }
