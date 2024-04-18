///*!
// * Bootstrap v3.3.6 (http://getbootstrap.com)
// * Copyright 2011-2015 Twitter, Inc.
// * Licensed under the MIT license
// */

//if (typeof jQuery === 'undefined') {
//  throw new Error('Bootstrap\'s JavaScript requires jQuery')
//}

//+function ($) {
//  'use strict';
//  var version = $.fn.jquery.split(' ')[0].split('.')
//  if ((version[0] < 2 && version[1] < 9) || (version[0] == 1 && version[1] == 9 && version[2] < 1) || (version[0] > 2)) {
//    throw new Error('Bootstrap\'s JavaScript requires jQuery version 1.9.1 or higher, but lower than version 3')
//  }
//}(jQuery);

///* ========================================================================
// * Bootstrap: transition.js v3.3.6
// * http://getbootstrap.com/javascript/#transitions
// * ========================================================================zz
// * Copyright 2011-2015 Twitter, Inc.
// * Licensed under MIT (https://github.com/twbs/bootstrap/blob/master/LICENSE)
// * ======================================================================== */


//+function ($) {
//  'use strict';

//  // CSS TRANSITION SUPPORT (Shoutout: http://www.modernizr.com/)
//  // ============================================================

//  function transitionEnd() {
//    var el = document.createElement('bootstrap')

//    var transEndEventNames = {
//      WebkitTransition : 'webkitTransitionEnd',
//      MozTransition    : 'transitionend',
//      OTransition      : 'oTransitionEnd otransitionend',
//      transition       : 'transitionend'
//    }

//    for (var name in transEndEventNames) {
//      if (el.style[name] !== undefined) {
//        return { end: transEndEventNames[name] }
//      }
//    }

//    return false // explicit for ie8 (  ._.)
//  }

//  // http://blog.alexmaccaw.com/css-transitions
//  $.fn.emulateTransitionEnd = function (duration) {
//    var called = false
//    var $el = this
//    $(this).one('bsTransitionEnd', function () { called = true })
//    var callback = function () { if (!called) $($el).trigger($.support.transition.end) }
//    setTimeout(callback, duration)
//    return this
//  }

//  $(function () {
//    $.support.transition = transitionEnd()

//    if (!$.support.transition) return

//    $.event.special.bsTransitionEnd = {
//      bindType: $.support.transition.end,
//      delegateType: $.support.transition.end,
//      handle: function (e) {
//        if ($(e.target).is(this)) return e.handleObj.handler.apply(this, arguments)
//      }
//    }
//  })

//}(jQuery);

///* ========================================================================
// * Bootstrap: alert.js v3.3.6
// * http://getbootstrap.com/javascript/#alerts
// * ========================================================================
// * Copyright 2011-2015 Twitter, Inc.
// * Licensed under MIT (https://github.com/twbs/bootstrap/blob/master/LICENSE)
// * ======================================================================== */


//+function ($) {
//  'use strict';

//  // ALERT CLASS DEFINITION
//  // ======================

//  var dismiss = '[data-dismiss="alert"]'
//  var Alert   = function (el) {
//    $(el).on('click', dismiss, this.close)
//  }

//  Alert.VERSION = '3.3.6'

//  Alert.TRANSITION_DURATION = 150

//  Alert.prototype.close = function (e) {
//    var $this    = $(this)
//    var selector = $this.attr('data-target')

//    if (!selector) {
//      selector = $this.attr('href')
//      selector = selector && selector.replace(/.*(?=#[^\s]*$)/, '') // strip for ie7
//    }

//    var $parent = $(selector)

//    if (e) e.preventDefault()

//    if (!$parent.length) {
//      $parent = $this.closest('.alert')
//    }

//    $parent.trigger(e = $.Event('close.bs.alert'))

//    if (e.isDefaultPrevented()) return

//    $parent.removeClass('in')

//    function removeElement() {
//      // detach from parent, fire event then clean up data
//      $parent.detach().trigger('closed.bs.alert').remove()
//    }

//    $.support.transition && $parent.hasClass('fade') ?
//      $parent
//        .one('bsTransitionEnd', removeElement)
//        .emulateTransitionEnd(Alert.TRANSITION_DURATION) :
//      removeElement()
//  }


//  // ALERT PLUGIN DEFINITION
//  // =======================

//  function Plugin(option) {
//    return this.each(function () {
//      var $this = $(this)
//      var data  = $this.data('bs.alert')

//      if (!data) $this.data('bs.alert', (data = new Alert(this)))
//      if (typeof option == 'string') data[option].call($this)
//    })
//  }

//  var old = $.fn.alert

//  $.fn.alert             = Plugin
//  $.fn.alert.Constructor = Alert


//  // ALERT NO CONFLICT
//  // =================

//  $.fn.alert.noConflict = function () {
//    $.fn.alert = old
//    return this
//  }


//  // ALERT DATA-API
//  // ==============

//  $(document).on('click.bs.alert.data-api', dismiss, Alert.prototype.close)

//}(jQuery);

///* ========================================================================
// * Bootstrap: button.js v3.3.6
// * http://getbootstrap.com/javascript/#buttons
// * ========================================================================
// * Copyright 2011-2015 Twitter, Inc.
// * Licensed under MIT (https://github.com/twbs/bootstrap/blob/master/LICENSE)
// * ======================================================================== */


//+function ($) {
//  'use strict';

//  // BUTTON PUBLIC CLASS DEFINITION
//  // ==============================

//  var Button = function (element, options) {
//    this.$element  = $(element)
//    this.options   = $.extend({}, Button.DEFAULTS, options)
//    this.isLoading = false
//  }

//  Button.VERSION  = '3.3.6'

//  Button.DEFAULTS = {
//    loadingText: 'loading...'
//  }

//  Button.prototype.setState = function (state) {
//    var d    = 'disabled'
//    var $el  = this.$element
//    var val  = $el.is('input') ? 'val' : 'html'
//    var data = $el.data()

//    state += 'Text'

//    if (data.resetText == null) $el.data('resetText', $el[val]())

//    // push to event loop to allow forms to submit
//    setTimeout($.proxy(function () {
//      $el[val](data[state] == null ? this.options[state] : data[state])

//      if (state == 'loadingText') {
//        this.isLoading = true
//        $el.addClass(d).attr(d, d)
//      } else if (this.isLoading) {
//        this.isLoading = false
//        $el.removeClass(d).removeAttr(d)
//      }
//    }, this), 0)
//  }

//  Button.prototype.toggle = function () {
//    var changed = true
//    var $parent = this.$element.closest('[data-toggle="buttons"]')

//    if ($parent.length) {
//      var $input = this.$element.find('input')
//      if ($input.prop('type') == 'radio') {
//        if ($input.prop('checked')) changed = false
//        $parent.find('.active').removeClass('active')
//        this.$element.addClass('active')
//      } else if ($input.prop('type') == 'checkbox') {
//        if (($input.prop('checked')) !== this.$element.hasClass('active')) changed = false
//        this.$element.toggleClass('active')
//      }
//      $input.prop('checked', this.$element.hasClass('active'))
//      if (changed) $input.trigger('change')
//    } else {
//      this.$element.attr('aria-pressed', !this.$element.hasClass('active'))
//      this.$element.toggleClass('active')
//    }
//  }


//  // BUTTON PLUGIN DEFINITION
//  // ========================

//  function Plugin(option) {
//    return this.each(function () {
//      var $this   = $(this)
//      var data    = $this.data('bs.button')
//      var options = typeof option == 'object' && option

//      if (!data) $this.data('bs.button', (data = new Button(this, options)))

//      if (option == 'toggle') data.toggle()
//      else if (option) data.setState(option)
//    })
//  }

//  var old = $.fn.button

//  $.fn.button             = Plugin
//  $.fn.button.Constructor = Button


//  // BUTTON NO CONFLICT
//  // ==================

//  $.fn.button.noConflict = function () {
//    $.fn.button = old
//    return this
//  }


//  // BUTTON DATA-API
//  // ===============

//  $(document)
//    .on('click.bs.button.data-api', '[data-toggle^="button"]', function (e) {
//      var $btn = $(e.target)
//      if (!$btn.hasClass('btn')) $btn = $btn.closest('.btn')
//      Plugin.call($btn, 'toggle')
//      if (!($(e.target).is('input[type="radio"]') || $(e.target).is('input[type="checkbox"]'))) e.preventDefault()
//    })
//    .on('focus.bs.button.data-api blur.bs.button.data-api', '[data-toggle^="button"]', function (e) {
//      $(e.target).closest('.btn').toggleClass('focus', /^focus(in)?$/.test(e.type))
//    })

//}(jQuery);

///* ========================================================================
// * Bootstrap: carousel.js v3.3.6
// * http://getbootstrap.com/javascript/#carousel
// * ========================================================================
// * Copyright 2011-2015 Twitter, Inc.
// * Licensed under MIT (https://github.com/twbs/bootstrap/blob/master/LICENSE)
// * ======================================================================== */


//+function ($) {
//  'use strict';

//  // CAROUSEL CLASS DEFINITION
//  // =========================

//  var Carousel = function (element, options) {
//    this.$element    = $(element)
//    this.$indicators = this.$element.find('.carousel-indicators')
//    this.options     = options
//    this.paused      = null
//    this.sliding     = null
//    this.interval    = null
//    this.$active     = null
//    this.$items      = null

//    this.options.keyboard && this.$element.on('keydown.bs.carousel', $.proxy(this.keydown, this))

//    this.options.pause == 'hover' && !('ontouchstart' in document.documentElement) && this.$element
//      .on('mouseenter.bs.carousel', $.proxy(this.pause, this))
//      .on('mouseleave.bs.carousel', $.proxy(this.cycle, this))
//  }

//  Carousel.VERSION  = '3.3.6'

//  Carousel.TRANSITION_DURATION = 600

//  Carousel.DEFAULTS = {
//    interval: 5000,
//    pause: 'hover',
//    wrap: true,
//    keyboard: true
//  }

//  Carousel.prototype.keydown = function (e) {
//    if (/input|textarea/i.test(e.target.tagName)) return
//    switch (e.which) {
//      case 37: this.prev(); break
//      case 39: this.next(); break
//      default: return
//    }

//    e.preventDefault()
//  }

//  Carousel.prototype.cycle = function (e) {
//    e || (this.paused = false)

//    this.interval && clearInterval(this.interval)

//    this.options.interval
//      && !this.paused
//      && (this.interval = setInterval($.proxy(this.next, this), this.options.interval))

//    return this
//  }

//  Carousel.prototype.getItemIndex = function (item) {
//    this.$items = item.parent().children('.item')
//    return this.$items.index(item || this.$active)
//  }

//  Carousel.prototype.getItemForDirection = function (direction, active) {
//    var activeIndex = this.getItemIndex(active)
//    var willWrap = (direction == 'prev' && activeIndex === 0)
//                || (direction == 'next' && activeIndex == (this.$items.length - 1))
//    if (willWrap && !this.options.wrap) return active
//    var delta = direction == 'prev' ? -1 : 1
//    var itemIndex = (activeIndex + delta) % this.$items.length
//    return this.$items.eq(itemIndex)
//  }

//  Carousel.prototype.to = function (pos) {
//    var that        = this
//    var activeIndex = this.getItemIndex(this.$active = this.$element.find('.item.active'))

//    if (pos > (this.$items.length - 1) || pos < 0) return

//    if (this.sliding)       return this.$element.one('slid.bs.carousel', function () { that.to(pos) }) // yes, "slid"
//    if (activeIndex == pos) return this.pause().cycle()

//    return this.slide(pos > activeIndex ? 'next' : 'prev', this.$items.eq(pos))
//  }

//  Carousel.prototype.pause = function (e) {
//    e || (this.paused = true)

//    if (this.$element.find('.next, .prev').length && $.support.transition) {
//      this.$element.trigger($.support.transition.end)
//      this.cycle(true)
//    }

//    this.interval = clearInterval(this.interval)

//    return this
//  }

//  Carousel.prototype.next = function () {
//    if (this.sliding) return
//    return this.slide('next')
//  }

//  Carousel.prototype.prev = function () {
//    if (this.sliding) return
//    return this.slide('prev')
//  }

//  Carousel.prototype.slide = function (type, next) {
//    var $active   = this.$element.find('.item.active')
//    var $next     = next || this.getItemForDirection(type, $active)
//    var isCycling = this.interval
//    var direction = type == 'next' ? 'left' : 'right'
//    var that      = this

//    if ($next.hasClass('active')) return (this.sliding = false)

//    var relatedTarget = $next[0]
//    var slideEvent = $.Event('slide.bs.carousel', {
//      relatedTarget: relatedTarget,
//      direction: direction
//    })
//    this.$element.trigger(slideEvent)
//    if (slideEvent.isDefaultPrevented()) return

//    this.sliding = true

//    isCycling && this.pause()

//    if (this.$indicators.length) {
//      this.$indicators.find('.active').removeClass('active')
//      var $nextIndicator = $(this.$indicators.children()[this.getItemIndex($next)])
//      $nextIndicator && $nextIndicator.addClass('active')
//    }

//    var slidEvent = $.Event('slid.bs.carousel', { relatedTarget: relatedTarget, direction: direction }) // yes, "slid"
//    if ($.support.transition && this.$element.hasClass('slide')) {
//      $next.addClass(type)
//      $next[0].offsetWidth // force reflow
//      $active.addClass(direction)
//      $next.addClass(direction)
//      $active
//        .one('bsTransitionEnd', function () {
//          $next.removeClass([type, direction].join(' ')).addClass('active')
//          $active.removeClass(['active', direction].join(' '))
//          that.sliding = false
//          setTimeout(function () {
//            that.$element.trigger(slidEvent)
//          }, 0)
//        })
//        .emulateTransitionEnd(Carousel.TRANSITION_DURATION)
//    } else {
//      $active.removeClass('active')
//      $next.addClass('active')
//      this.sliding = false
//      this.$element.trigger(slidEvent)
//    }

//    isCycling && this.cycle()

//    return this
//  }


//  // CAROUSEL PLUGIN DEFINITION
//  // ==========================

//  function Plugin(option) {
//    return this.each(function () {
//      var $this   = $(this)
//      var data    = $this.data('bs.carousel')
//      var options = $.extend({}, Carousel.DEFAULTS, $this.data(), typeof option == 'object' && option)
//      var action  = typeof option == 'string' ? option : options.slide

//      if (!data) $this.data('bs.carousel', (data = new Carousel(this, options)))
//      if (typeof option == 'number') data.to(option)
//      else if (action) data[action]()
//      else if (options.interval) data.pause().cycle()
//    })
//  }

//  var old = $.fn.carousel

//  $.fn.carousel             = Plugin
//  $.fn.carousel.Constructor = Carousel


//  // CAROUSEL NO CONFLICT
//  // ====================

//  $.fn.carousel.noConflict = function () {
//    $.fn.carousel = old
//    return this
//  }


//  // CAROUSEL DATA-API
//  // =================

//  var clickHandler = function (e) {
//    var href
//    var $this   = $(this)
//    var $target = $($this.attr('data-target') || (href = $this.attr('href')) && href.replace(/.*(?=#[^\s]+$)/, '')) // strip for ie7
//    if (!$target.hasClass('carousel')) return
//    var options = $.extend({}, $target.data(), $this.data())
//    var slideIndex = $this.attr('data-slide-to')
//    if (slideIndex) options.interval = false

//    Plugin.call($target, options)

//    if (slideIndex) {
//      $target.data('bs.carousel').to(slideIndex)
//    }

//    e.preventDefault()
//  }

//  $(document)
//    .on('click.bs.carousel.data-api', '[data-slide]', clickHandler)
//    .on('click.bs.carousel.data-api', '[data-slide-to]', clickHandler)

//  $(window).on('load', function () {
//    $('[data-ride="carousel"]').each(function () {
//      var $carousel = $(this)
//      Plugin.call($carousel, $carousel.data())
//    })
//  })

//}(jQuery);

///* ========================================================================
// * Bootstrap: collapse.js v3.3.6
// * http://getbootstrap.com/javascript/#collapse
// * ========================================================================
// * Copyright 2011-2015 Twitter, Inc.
// * Licensed under MIT (https://github.com/twbs/bootstrap/blob/master/LICENSE)
// * ======================================================================== */


//+function ($) {
//  'use strict';

//  // COLLAPSE PUBLIC CLASS DEFINITION
//  // ================================

//  var Collapse = function (element, options) {
//    this.$element      = $(element)
//    this.options       = $.extend({}, Collapse.DEFAULTS, options)
//    this.$trigger      = $('[data-toggle="collapse"][href="#' + element.id + '"],' +
//                           '[data-toggle="collapse"][data-target="#' + element.id + '"]')
//    this.transitioning = null

//    if (this.options.parent) {
//      this.$parent = this.getParent()
//    } else {
//      this.addAriaAndCollapsedClass(this.$element, this.$trigger)
//    }

//    if (this.options.toggle) this.toggle()
//  }

//  Collapse.VERSION  = '3.3.6'

//  Collapse.TRANSITION_DURATION = 350

//  Collapse.DEFAULTS = {
//    toggle: true
//  }

//  Collapse.prototype.dimension = function () {
//    var hasWidth = this.$element.hasClass('width')
//    return hasWidth ? 'width' : 'height'
//  }

//  Collapse.prototype.show = function () {
//    if (this.transitioning || this.$element.hasClass('in')) return

//    var activesData
//    var actives = this.$parent && this.$parent.children('.panel').children('.in, .collapsing')

//    if (actives && actives.length) {
//      activesData = actives.data('bs.collapse')
//      if (activesData && activesData.transitioning) return
//    }

//    var startEvent = $.Event('show.bs.collapse')
//    this.$element.trigger(startEvent)
//    if (startEvent.isDefaultPrevented()) return

//    if (actives && actives.length) {
//      Plugin.call(actives, 'hide')
//      activesData || actives.data('bs.collapse', null)
//    }

//    var dimension = this.dimension()

//    this.$element
//      .removeClass('collapse')
//      .addClass('collapsing')[dimension](0)
//      .attr('aria-expanded', true)

//    this.$trigger
//      .removeClass('collapsed')
//      .attr('aria-expanded', true)

//    this.transitioning = 1

//    var complete = function () {
//      this.$element
//        .removeClass('collapsing')
//        .addClass('collapse in')[dimension]('')
//      this.transitioning = 0
//      this.$element
//        .trigger('shown.bs.collapse')
//    }

//    if (!$.support.transition) return complete.call(this)

//    var scrollSize = $.camelCase(['scroll', dimension].join('-'))

//    this.$element
//      .one('bsTransitionEnd', $.proxy(complete, this))
//      .emulateTransitionEnd(Collapse.TRANSITION_DURATION)[dimension](this.$element[0][scrollSize])
//  }

//  Collapse.prototype.hide = function () {
//    if (this.transitioning || !this.$element.hasClass('in')) return

//    var startEvent = $.Event('hide.bs.collapse')
//    this.$element.trigger(startEvent)
//    if (startEvent.isDefaultPrevented()) return

//    var dimension = this.dimension()

//    this.$element[dimension](this.$element[dimension]())[0].offsetHeight

//    this.$element
//      .addClass('collapsing')
//      .removeClass('collapse in')
//      .attr('aria-expanded', false)

//    this.$trigger
//      .addClass('collapsed')
//      .attr('aria-expanded', false)

//    this.transitioning = 1

//    var complete = function () {
//      this.transitioning = 0
//      this.$element
//        .removeClass('collapsing')
//        .addClass('collapse')
//        .trigger('hidden.bs.collapse')
//    }

//    if (!$.support.transition) return complete.call(this)

//    this.$element
//      [dimension](0)
//      .one('bsTransitionEnd', $.proxy(complete, this))
//      .emulateTransitionEnd(Collapse.TRANSITION_DURATION)
//  }

//  Collapse.prototype.toggle = function () {
//    this[this.$element.hasClass('in') ? 'hide' : 'show']()
//  }

//  Collapse.prototype.getParent = function () {
//    return $(this.options.parent)
//      .find('[data-toggle="collapse"][data-parent="' + this.options.parent + '"]')
//      .each($.proxy(function (i, element) {
//        var $element = $(element)
//        this.addAriaAndCollapsedClass(getTargetFromTrigger($element), $element)
//      }, this))
//      .end()
//  }

//  Collapse.prototype.addAriaAndCollapsedClass = function ($element, $trigger) {
//    var isOpen = $element.hasClass('in')

//    $element.attr('aria-expanded', isOpen)
//    $trigger
//      .toggleClass('collapsed', !isOpen)
//      .attr('aria-expanded', isOpen)
//  }

//  function getTargetFromTrigger($trigger) {
//    var href
//    var target = $trigger.attr('data-target')
//      || (href = $trigger.attr('href')) && href.replace(/.*(?=#[^\s]+$)/, '') // strip for ie7

//    return $(target)
//  }


//  // COLLAPSE PLUGIN DEFINITION
//  // ==========================

//  function Plugin(option) {
//    return this.each(function () {
//      var $this   = $(this)
//      var data    = $this.data('bs.collapse')
//      var options = $.extend({}, Collapse.DEFAULTS, $this.data(), typeof option == 'object' && option)

//      if (!data && options.toggle && /show|hide/.test(option)) options.toggle = false
//      if (!data) $this.data('bs.collapse', (data = new Collapse(this, options)))
//      if (typeof option == 'string') data[option]()
//    })
//  }

//  var old = $.fn.collapse

//  $.fn.collapse             = Plugin
//  $.fn.collapse.Constructor = Collapse


//  // COLLAPSE NO CONFLICT
//  // ====================

//  $.fn.collapse.noConflict = function () {
//    $.fn.collapse = old
//    return this
//  }


//  // COLLAPSE DATA-API
//  // =================

//  $(document).on('click.bs.collapse.data-api', '[data-toggle="collapse"]', function (e) {
//    var $this   = $(this)

//    if (!$this.attr('data-target')) e.preventDefault()

//    var $target = getTargetFromTrigger($this)
//    var data    = $target.data('bs.collapse')
//    var option  = data ? 'toggle' : $this.data()

//    Plugin.call($target, option)
//  })

//}(jQuery);

///* ========================================================================
// * Bootstrap: dropdown.js v3.3.6
// * http://getbootstrap.com/javascript/#dropdowns
// * ========================================================================
// * Copyright 2011-2015 Twitter, Inc.
// * Licensed under MIT (https://github.com/twbs/bootstrap/blob/master/LICENSE)
// * ======================================================================== */


//+function ($) {
//  'use strict';

//  // DROPDOWN CLASS DEFINITION
//  // =========================

//  var backdrop = '.dropdown-backdrop'
//  var toggle   = '[data-toggle="dropdown"]'
//  var Dropdown = function (element) {
//    $(element).on('click.bs.dropdown', this.toggle)
//  }

//  Dropdown.VERSION = '3.3.6'

//  function getParent($this) {
//    var selector = $this.attr('data-target')

//    if (!selector) {
//      selector = $this.attr('href')
//      selector = selector && /#[A-Za-z]/.test(selector) && selector.replace(/.*(?=#[^\s]*$)/, '') // strip for ie7
//    }

//    var $parent = selector && $(selector)

//    return $parent && $parent.length ? $parent : $this.parent()
//  }

//  function clearMenus(e) {
//    if (e && e.which === 3) return
//    $(backdrop).remove()
//    $(toggle).each(function () {
//      var $this         = $(this)
//      var $parent       = getParent($this)
//      var relatedTarget = { relatedTarget: this }

//      if (!$parent.hasClass('open')) return

//      if (e && e.type == 'click' && /input|textarea/i.test(e.target.tagName) && $.contains($parent[0], e.target)) return

//      $parent.trigger(e = $.Event('hide.bs.dropdown', relatedTarget))

//      if (e.isDefaultPrevented()) return

//      $this.attr('aria-expanded', 'false')
//      $parent.removeClass('open').trigger($.Event('hidden.bs.dropdown', relatedTarget))
//    })
//  }

//  Dropdown.prototype.toggle = function (e) {
//    var $this = $(this)

//    if ($this.is('.disabled, :disabled')) return

//    var $parent  = getParent($this)
//    var isActive = $parent.hasClass('open')

//    clearMenus()

//    if (!isActive) {
//      if ('ontouchstart' in document.documentElement && !$parent.closest('.navbar-nav').length) {
//        // if mobile we use a backdrop because click events don't delegate
//        $(document.createElement('div'))
//          .addClass('dropdown-backdrop')
//          .insertAfter($(this))
//          .on('click', clearMenus)
//      }

//      var relatedTarget = { relatedTarget: this }
//      $parent.trigger(e = $.Event('show.bs.dropdown', relatedTarget))

//      if (e.isDefaultPrevented()) return

//      $this
//        .trigger('focus')
//        .attr('aria-expanded', 'true')

//      $parent
//        .toggleClass('open')
//        .trigger($.Event('shown.bs.dropdown', relatedTarget))
//    }

//    return false
//  }

//  Dropdown.prototype.keydown = function (e) {
//    if (!/(38|40|27|32)/.test(e.which) || /input|textarea/i.test(e.target.tagName)) return

//    var $this = $(this)

//    e.preventDefault()
//    e.stopPropagation()

//    if ($this.is('.disabled, :disabled')) return

//    var $parent  = getParent($this)
//    var isActive = $parent.hasClass('open')

//    if (!isActive && e.which != 27 || isActive && e.which == 27) {
//      if (e.which == 27) $parent.find(toggle).trigger('focus')
//      return $this.trigger('click')
//    }

//    var desc = ' li:not(.disabled):visible a'
//    var $items = $parent.find('.dropdown-menu' + desc)

//    if (!$items.length) return

//    var index = $items.index(e.target)

//    if (e.which == 38 && index > 0)                 index--         // up
//    if (e.which == 40 && index < $items.length - 1) index++         // down
//    if (!~index)                                    index = 0

//    $items.eq(index).trigger('focus')
//  }


//  // DROPDOWN PLUGIN DEFINITION
//  // ==========================

//  function Plugin(option) {
//    return this.each(function () {
//      var $this = $(this)
//      var data  = $this.data('bs.dropdown')

//      if (!data) $this.data('bs.dropdown', (data = new Dropdown(this)))
//      if (typeof option == 'string') data[option].call($this)
//    })
//  }

//  var old = $.fn.dropdown

//  $.fn.dropdown             = Plugin
//  $.fn.dropdown.Constructor = Dropdown


//  // DROPDOWN NO CONFLICT
//  // ====================

//  $.fn.dropdown.noConflict = function () {
//    $.fn.dropdown = old
//    return this
//  }


//  // APPLY TO STANDARD DROPDOWN ELEMENTS
//  // ===================================

//  $(document)
//    .on('click.bs.dropdown.data-api', clearMenus)
//    .on('click.bs.dropdown.data-api', '.dropdown form', function (e) { e.stopPropagation() })
//    .on('click.bs.dropdown.data-api', toggle, Dropdown.prototype.toggle)
//    .on('keydown.bs.dropdown.data-api', toggle, Dropdown.prototype.keydown)
//    .on('keydown.bs.dropdown.data-api', '.dropdown-menu', Dropdown.prototype.keydown)

//}(jQuery);

///* ========================================================================
// * Bootstrap: modal.js v3.3.6
// * http://getbootstrap.com/javascript/#modals
// * ========================================================================
// * Copyright 2011-2015 Twitter, Inc.
// * Licensed under MIT (https://github.com/twbs/bootstrap/blob/master/LICENSE)
// * ======================================================================== */


//+function ($) {
//  'use strict';

//  // MODAL CLASS DEFINITION
//  // ======================

//  var Modal = function (element, options) {
//    this.options             = options
//    this.$body               = $(document.body)
//    this.$element            = $(element)
//    this.$dialog             = this.$element.find('.modal-dialog')
//    this.$backdrop           = null
//    this.isShown             = null
//    this.originalBodyPad     = null
//    this.scrollbarWidth      = 0
//    this.ignoreBackdropClick = false

//    if (this.options.remote) {
//      this.$element
//        .find('.modal-content')
//        .load(this.options.remote, $.proxy(function () {
//          this.$element.trigger('loaded.bs.modal')
//        }, this))
//    }
//  }

//  Modal.VERSION  = '3.3.6'

//  Modal.TRANSITION_DURATION = 300
//  Modal.BACKDROP_TRANSITION_DURATION = 150

//  Modal.DEFAULTS = {
//    backdrop: true,
//    keyboard: true,
//    show: true
//  }

//  Modal.prototype.toggle = function (_relatedTarget) {
//    return this.isShown ? this.hide() : this.show(_relatedTarget)
//  }

//  Modal.prototype.show = function (_relatedTarget) {
//    var that = this
//    var e    = $.Event('show.bs.modal', { relatedTarget: _relatedTarget })

//    this.$element.trigger(e)

//    if (this.isShown || e.isDefaultPrevented()) return

//    this.isShown = true

//    this.checkScrollbar()
//    this.setScrollbar()
//    this.$body.addClass('modal-open')

//    this.escape()
//    this.resize()

//    this.$element.on('click.dismiss.bs.modal', '[data-dismiss="modal"]', $.proxy(this.hide, this))

//    this.$dialog.on('mousedown.dismiss.bs.modal', function () {
//      that.$element.one('mouseup.dismiss.bs.modal', function (e) {
//        if ($(e.target).is(that.$element)) that.ignoreBackdropClick = true
//      })
//    })

//    this.backdrop(function () {
//      var transition = $.support.transition && that.$element.hasClass('fade')

//      if (!that.$element.parent().length) {
//        that.$element.appendTo(that.$body) // don't move modals dom position
//      }

//      that.$element
//        .show()
//        .scrollTop(0)

//      that.adjustDialog()

//      if (transition) {
//        that.$element[0].offsetWidth // force reflow
//      }

//      that.$element.addClass('in')

//      that.enforceFocus()

//      var e = $.Event('shown.bs.modal', { relatedTarget: _relatedTarget })

//      transition ?
//        that.$dialog // wait for modal to slide in
//          .one('bsTransitionEnd', function () {
//            that.$element.trigger('focus').trigger(e)
//          })
//          .emulateTransitionEnd(Modal.TRANSITION_DURATION) :
//        that.$element.trigger('focus').trigger(e)
//    })
//  }

//  Modal.prototype.hide = function (e) {
//    if (e) e.preventDefault()

//    e = $.Event('hide.bs.modal')

//    this.$element.trigger(e)

//    if (!this.isShown || e.isDefaultPrevented()) return

//    this.isShown = false

//    this.escape()
//    this.resize()

//    $(document).off('focusin.bs.modal')

//    this.$element
//      .removeClass('in')
//      .off('click.dismiss.bs.modal')
//      .off('mouseup.dismiss.bs.modal')

//    this.$dialog.off('mousedown.dismiss.bs.modal')

//    $.support.transition && this.$element.hasClass('fade') ?
//      this.$element
//        .one('bsTransitionEnd', $.proxy(this.hideModal, this))
//        .emulateTransitionEnd(Modal.TRANSITION_DURATION) :
//      this.hideModal()
//  }

//  Modal.prototype.enforceFocus = function () {
//    $(document)
//      .off('focusin.bs.modal') // guard against infinite focus loop
//      .on('focusin.bs.modal', $.proxy(function (e) {
//        if (this.$element[0] !== e.target && !this.$element.has(e.target).length) {
//          this.$element.trigger('focus')
//        }
//      }, this))
//  }

//  Modal.prototype.escape = function () {
//    if (this.isShown && this.options.keyboard) {
//      this.$element.on('keydown.dismiss.bs.modal', $.proxy(function (e) {
//        e.which == 27 && this.hide()
//      }, this))
//    } else if (!this.isShown) {
//      this.$element.off('keydown.dismiss.bs.modal')
//    }
//  }

//  Modal.prototype.resize = function () {
//    if (this.isShown) {
//      $(window).on('resize.bs.modal', $.proxy(this.handleUpdate, this))
//    } else {
//      $(window).off('resize.bs.modal')
//    }
//  }

//  Modal.prototype.hideModal = function () {
//    var that = this
//    this.$element.hide()
//    this.backdrop(function () {
//      that.$body.removeClass('modal-open')
//      that.resetAdjustments()
//      that.resetScrollbar()
//      that.$element.trigger('hidden.bs.modal')
//    })
//  }

//  Modal.prototype.removeBackdrop = function () {
//    this.$backdrop && this.$backdrop.remove()
//    this.$backdrop = null
//  }

//  Modal.prototype.backdrop = function (callback) {
//    var that = this
//    var animate = this.$element.hasClass('fade') ? 'fade' : ''

//    if (this.isShown && this.options.backdrop) {
//      var doAnimate = $.support.transition && animate

//      this.$backdrop = $(document.createElement('div'))
//        .addClass('modal-backdrop ' + animate)
//        .appendTo(this.$body)

//      this.$element.on('click.dismiss.bs.modal', $.proxy(function (e) {
//        if (this.ignoreBackdropClick) {
//          this.ignoreBackdropClick = false
//          return
//        }
//        if (e.target !== e.currentTarget) return
//        this.options.backdrop == 'static'
//          ? this.$element[0].focus()
//          : this.hide()
//      }, this))

//      if (doAnimate) this.$backdrop[0].offsetWidth // force reflow

//      this.$backdrop.addClass('in')

//      if (!callback) return

//      doAnimate ?
//        this.$backdrop
//          .one('bsTransitionEnd', callback)
//          .emulateTransitionEnd(Modal.BACKDROP_TRANSITION_DURATION) :
//        callback()

//    } else if (!this.isShown && this.$backdrop) {
//      this.$backdrop.removeClass('in')

//      var callbackRemove = function () {
//        that.removeBackdrop()
//        callback && callback()
//      }
//      $.support.transition && this.$element.hasClass('fade') ?
//        this.$backdrop
//          .one('bsTransitionEnd', callbackRemove)
//          .emulateTransitionEnd(Modal.BACKDROP_TRANSITION_DURATION) :
//        callbackRemove()

//    } else if (callback) {
//      callback()
//    }
//  }

//  // these following methods are used to handle overflowing modals

//  Modal.prototype.handleUpdate = function () {
//    this.adjustDialog()
//  }

//  Modal.prototype.adjustDialog = function () {
//    var modalIsOverflowing = this.$element[0].scrollHeight > document.documentElement.clientHeight

//    this.$element.css({
//      paddingLeft:  !this.bodyIsOverflowing && modalIsOverflowing ? this.scrollbarWidth : '',
//      paddingRight: this.bodyIsOverflowing && !modalIsOverflowing ? this.scrollbarWidth : ''
//    })
//  }

//  Modal.prototype.resetAdjustments = function () {
//    this.$element.css({
//      paddingLeft: '',
//      paddingRight: ''
//    })
//  }

//  Modal.prototype.checkScrollbar = function () {
//    var fullWindowWidth = window.innerWidth
//    if (!fullWindowWidth) { // workaround for missing window.innerWidth in IE8
//      var documentElementRect = document.documentElement.getBoundingClientRect()
//      fullWindowWidth = documentElementRect.right - Math.abs(documentElementRect.left)
//    }
//    this.bodyIsOverflowing = document.body.clientWidth < fullWindowWidth
//    this.scrollbarWidth = this.measureScrollbar()
//  }

//  Modal.prototype.setScrollbar = function () {
//    var bodyPad = parseInt((this.$body.css('padding-right') || 0), 10)
//    this.originalBodyPad = document.body.style.paddingRight || ''
//    if (this.bodyIsOverflowing) this.$body.css('padding-right', bodyPad + this.scrollbarWidth)
//  }

//  Modal.prototype.resetScrollbar = function () {
//    this.$body.css('padding-right', this.originalBodyPad)
//  }

//  Modal.prototype.measureScrollbar = function () { // thx walsh
//    var scrollDiv = document.createElement('div')
//    scrollDiv.className = 'modal-scrollbar-measure'
//    this.$body.append(scrollDiv)
//    var scrollbarWidth = scrollDiv.offsetWidth - scrollDiv.clientWidth
//    this.$body[0].removeChild(scrollDiv)
//    return scrollbarWidth
//  }


//  // MODAL PLUGIN DEFINITION
//  // =======================

//  function Plugin(option, _relatedTarget) {
//    return this.each(function () {
//      var $this   = $(this)
//      var data    = $this.data('bs.modal')
//      var options = $.extend({}, Modal.DEFAULTS, $this.data(), typeof option == 'object' && option)

//      if (!data) $this.data('bs.modal', (data = new Modal(this, options)))
//      if (typeof option == 'string') data[option](_relatedTarget)
//      else if (options.show) data.show(_relatedTarget)
//    })
//  }

//  var old = $.fn.modal

//  $.fn.modal             = Plugin
//  $.fn.modal.Constructor = Modal


//  // MODAL NO CONFLICT
//  // =================

//  $.fn.modal.noConflict = function () {
//    $.fn.modal = old
//    return this
//  }


//  // MODAL DATA-API
//  // ==============

//  $(document).on('click.bs.modal.data-api', '[data-toggle="modal"]', function (e) {
//    var $this   = $(this)
//    var href    = $this.attr('href')
//    var $target = $($this.attr('data-target') || (href && href.replace(/.*(?=#[^\s]+$)/, ''))) // strip for ie7
//    var option  = $target.data('bs.modal') ? 'toggle' : $.extend({ remote: !/#/.test(href) && href }, $target.data(), $this.data())

//    if ($this.is('a')) e.preventDefault()

//    $target.one('show.bs.modal', function (showEvent) {
//      if (showEvent.isDefaultPrevented()) return // only register focus restorer if modal will actually get shown
//      $target.one('hidden.bs.modal', function () {
//        $this.is(':visible') && $this.trigger('focus')
//      })
//    })
//    Plugin.call($target, option, this)
//  })

//}(jQuery);

///* ========================================================================
// * Bootstrap: tooltip.js v3.3.6
// * http://getbootstrap.com/javascript/#tooltip
// * Inspired by the original jQuery.tipsy by Jason Frame
// * ========================================================================
// * Copyright 2011-2015 Twitter, Inc.
// * Licensed under MIT (https://github.com/twbs/bootstrap/blob/master/LICENSE)
// * ======================================================================== */


//+function ($) {
//  'use strict';

//  // TOOLTIP PUBLIC CLASS DEFINITION
//  // ===============================

//  var Tooltip = function (element, options) {
//    this.type       = null
//    this.options    = null
//    this.enabled    = null
//    this.timeout    = null
//    this.hoverState = null
//    this.$element   = null
//    this.inState    = null

//    this.init('tooltip', element, options)
//  }

//  Tooltip.VERSION  = '3.3.6'

//  Tooltip.TRANSITION_DURATION = 150

//  Tooltip.DEFAULTS = {
//    animation: true,
//    placement: 'top',
//    selector: false,
//    template: '<div class="tooltip" role="tooltip"><div class="tooltip-arrow"></div><div class="tooltip-inner"></div></div>',
//    trigger: 'hover focus',
//    title: '',
//    delay: 0,
//    html: false,
//    container: false,
//    viewport: {
//      selector: 'body',
//      padding: 0
//    }
//  }

//  Tooltip.prototype.init = function (type, element, options) {
//    this.enabled   = true
//    this.type      = type
//    this.$element  = $(element)
//    this.options   = this.getOptions(options)
//    this.$viewport = this.options.viewport && $($.isFunction(this.options.viewport) ? this.options.viewport.call(this, this.$element) : (this.options.viewport.selector || this.options.viewport))
//    this.inState   = { click: false, hover: false, focus: false }

//    if (this.$element[0] instanceof document.constructor && !this.options.selector) {
//      throw new Error('`selector` option must be specified when initializing ' + this.type + ' on the window.document object!')
//    }

//    var triggers = this.options.trigger.split(' ')

//    for (var i = triggers.length; i--;) {
//      var trigger = triggers[i]

//      if (trigger == 'click') {
//        this.$element.on('click.' + this.type, this.options.selector, $.proxy(this.toggle, this))
//      } else if (trigger != 'manual') {
//        var eventIn  = trigger == 'hover' ? 'mouseenter' : 'focusin'
//        var eventOut = trigger == 'hover' ? 'mouseleave' : 'focusout'

//        this.$element.on(eventIn  + '.' + this.type, this.options.selector, $.proxy(this.enter, this))
//        this.$element.on(eventOut + '.' + this.type, this.options.selector, $.proxy(this.leave, this))
//      }
//    }

//    this.options.selector ?
//      (this._options = $.extend({}, this.options, { trigger: 'manual', selector: '' })) :
//      this.fixTitle()
//  }

//  Tooltip.prototype.getDefaults = function () {
//    return Tooltip.DEFAULTS
//  }

//  Tooltip.prototype.getOptions = function (options) {
//    options = $.extend({}, this.getDefaults(), this.$element.data(), options)

//    if (options.delay && typeof options.delay == 'number') {
//      options.delay = {
//        show: options.delay,
//        hide: options.delay
//      }
//    }

//    return options
//  }

//  Tooltip.prototype.getDelegateOptions = function () {
//    var options  = {}
//    var defaults = this.getDefaults()

//    this._options && $.each(this._options, function (key, value) {
//      if (defaults[key] != value) options[key] = value
//    })

//    return options
//  }

//  Tooltip.prototype.enter = function (obj) {
//    var self = obj instanceof this.constructor ?
//      obj : $(obj.currentTarget).data('bs.' + this.type)

//    if (!self) {
//      self = new this.constructor(obj.currentTarget, this.getDelegateOptions())
//      $(obj.currentTarget).data('bs.' + this.type, self)
//    }

//    if (obj instanceof $.Event) {
//      self.inState[obj.type == 'focusin' ? 'focus' : 'hover'] = true
//    }

//    if (self.tip().hasClass('in') || self.hoverState == 'in') {
//      self.hoverState = 'in'
//      return
//    }

//    clearTimeout(self.timeout)

//    self.hoverState = 'in'

//    if (!self.options.delay || !self.options.delay.show) return self.show()

//    self.timeout = setTimeout(function () {
//      if (self.hoverState == 'in') self.show()
//    }, self.options.delay.show)
//  }

//  Tooltip.prototype.isInStateTrue = function () {
//    for (var key in this.inState) {
//      if (this.inState[key]) return true
//    }

//    return false
//  }

//  Tooltip.prototype.leave = function (obj) {
//    var self = obj instanceof this.constructor ?
//      obj : $(obj.currentTarget).data('bs.' + this.type)

//    if (!self) {
//      self = new this.constructor(obj.currentTarget, this.getDelegateOptions())
//      $(obj.currentTarget).data('bs.' + this.type, self)
//    }

//    if (obj instanceof $.Event) {
//      self.inState[obj.type == 'focusout' ? 'focus' : 'hover'] = false
//    }

//    if (self.isInStateTrue()) return

//    clearTimeout(self.timeout)

//    self.hoverState = 'out'

//    if (!self.options.delay || !self.options.delay.hide) return self.hide()

//    self.timeout = setTimeout(function () {
//      if (self.hoverState == 'out') self.hide()
//    }, self.options.delay.hide)
//  }

//  Tooltip.prototype.show = function () {
//    var e = $.Event('show.bs.' + this.type)

//    if (this.hasContent() && this.enabled) {
//      this.$element.trigger(e)

//      var inDom = $.contains(this.$element[0].ownerDocument.documentElement, this.$element[0])
//      if (e.isDefaultPrevented() || !inDom) return
//      var that = this

//      var $tip = this.tip()

//      var tipId = this.getUID(this.type)

//      this.setContent()
//      $tip.attr('id', tipId)
//      this.$element.attr('aria-describedby', tipId)

//      if (this.options.animation) $tip.addClass('fade')

//      var placement = typeof this.options.placement == 'function' ?
//        this.options.placement.call(this, $tip[0], this.$element[0]) :
//        this.options.placement

//      var autoToken = /\s?auto?\s?/i
//      var autoPlace = autoToken.test(placement)
//      if (autoPlace) placement = placement.replace(autoToken, '') || 'top'

//      $tip
//        .detach()
//        .css({ top: 0, left: 0, display: 'block' })
//        .addClass(placement)
//        .data('bs.' + this.type, this)

//      this.options.container ? $tip.appendTo(this.options.container) : $tip.insertAfter(this.$element)
//      this.$element.trigger('inserted.bs.' + this.type)

//      var pos          = this.getPosition()
//      var actualWidth  = $tip[0].offsetWidth
//      var actualHeight = $tip[0].offsetHeight

//      if (autoPlace) {
//        var orgPlacement = placement
//        var viewportDim = this.getPosition(this.$viewport)

//        placement = placement == 'bottom' && pos.bottom + actualHeight > viewportDim.bottom ? 'top'    :
//                    placement == 'top'    && pos.top    - actualHeight < viewportDim.top    ? 'bottom' :
//                    placement == 'right'  && pos.right  + actualWidth  > viewportDim.width  ? 'left'   :
//                    placement == 'left'   && pos.left   - actualWidth  < viewportDim.left   ? 'right'  :
//                    placement

//        $tip
//          .removeClass(orgPlacement)
//          .addClass(placement)
//      }

//      var calculatedOffset = this.getCalculatedOffset(placement, pos, actualWidth, actualHeight)

//      this.applyPlacement(calculatedOffset, placement)

//      var complete = function () {
//        var prevHoverState = that.hoverState
//        that.$element.trigger('shown.bs.' + that.type)
//        that.hoverState = null

//        if (prevHoverState == 'out') that.leave(that)
//      }

//      $.support.transition && this.$tip.hasClass('fade') ?
//        $tip
//          .one('bsTransitionEnd', complete)
//          .emulateTransitionEnd(Tooltip.TRANSITION_DURATION) :
//        complete()
//    }
//  }

//  Tooltip.prototype.applyPlacement = function (offset, placement) {
//    var $tip   = this.tip()
//    var width  = $tip[0].offsetWidth
//    var height = $tip[0].offsetHeight

//    // manually read margins because getBoundingClientRect includes difference
//    var marginTop = parseInt($tip.css('margin-top'), 10)
//    var marginLeft = parseInt($tip.css('margin-left'), 10)

//    // we must check for NaN for ie 8/9
//    if (isNaN(marginTop))  marginTop  = 0
//    if (isNaN(marginLeft)) marginLeft = 0

//    offset.top  += marginTop
//    offset.left += marginLeft

//    // $.fn.offset doesn't round pixel values
//    // so we use setOffset directly with our own function B-0
//    $.offset.setOffset($tip[0], $.extend({
//      using: function (props) {
//        $tip.css({
//          top: Math.round(props.top),
//          left: Math.round(props.left)
//        })
//      }
//    }, offset), 0)

//    $tip.addClass('in')

//    // check to see if placing tip in new offset caused the tip to resize itself
//    var actualWidth  = $tip[0].offsetWidth
//    var actualHeight = $tip[0].offsetHeight

//    if (placement == 'top' && actualHeight != height) {
//      offset.top = offset.top + height - actualHeight
//    }

//    var delta = this.getViewportAdjustedDelta(placement, offset, actualWidth, actualHeight)

//    if (delta.left) offset.left += delta.left
//    else offset.top += delta.top

//    var isVertical          = /top|bottom/.test(placement)
//    var arrowDelta          = isVertical ? delta.left * 2 - width + actualWidth : delta.top * 2 - height + actualHeight
//    var arrowOffsetPosition = isVertical ? 'offsetWidth' : 'offsetHeight'

//    $tip.offset(offset)
//    this.replaceArrow(arrowDelta, $tip[0][arrowOffsetPosition], isVertical)
//  }

//  Tooltip.prototype.replaceArrow = function (delta, dimension, isVertical) {
//    this.arrow()
//      .css(isVertical ? 'left' : 'top', 50 * (1 - delta / dimension) + '%')
//      .css(isVertical ? 'top' : 'left', '')
//  }

//  Tooltip.prototype.setContent = function () {
//    var $tip  = this.tip()
//    var title = this.getTitle()

//    $tip.find('.tooltip-inner')[this.options.html ? 'html' : 'text'](title)
//    $tip.removeClass('fade in top bottom left right')
//  }

//  Tooltip.prototype.hide = function (callback) {
//    var that = this
//    var $tip = $(this.$tip)
//    var e    = $.Event('hide.bs.' + this.type)

//    function complete() {
//      if (that.hoverState != 'in') $tip.detach()
//      that.$element
//        .removeAttr('aria-describedby')
//        .trigger('hidden.bs.' + that.type)
//      callback && callback()
//    }

//    this.$element.trigger(e)

//    if (e.isDefaultPrevented()) return

//    $tip.removeClass('in')

//    $.support.transition && $tip.hasClass('fade') ?
//      $tip
//        .one('bsTransitionEnd', complete)
//        .emulateTransitionEnd(Tooltip.TRANSITION_DURATION) :
//      complete()

//    this.hoverState = null

//    return this
//  }

//  Tooltip.prototype.fixTitle = function () {
//    var $e = this.$element
//    if ($e.attr('title') || typeof $e.attr('data-original-title') != 'string') {
//      $e.attr('data-original-title', $e.attr('title') || '').attr('title', '')
//    }
//  }

//  Tooltip.prototype.hasContent = function () {
//    return this.getTitle()
//  }

//  Tooltip.prototype.getPosition = function ($element) {
//    $element   = $element || this.$element

//    var el     = $element[0]
//    var isBody = el.tagName == 'BODY'

//    var elRect    = el.getBoundingClientRect()
//    if (elRect.width == null) {
//      // width and height are missing in IE8, so compute them manually; see https://github.com/twbs/bootstrap/issues/14093
//      elRect = $.extend({}, elRect, { width: elRect.right - elRect.left, height: elRect.bottom - elRect.top })
//    }
//    var elOffset  = isBody ? { top: 0, left: 0 } : $element.offset()
//    var scroll    = { scroll: isBody ? document.documentElement.scrollTop || document.body.scrollTop : $element.scrollTop() }
//    var outerDims = isBody ? { width: $(window).width(), height: $(window).height() } : null

//    return $.extend({}, elRect, scroll, outerDims, elOffset)
//  }

//  Tooltip.prototype.getCalculatedOffset = function (placement, pos, actualWidth, actualHeight) {
//    return placement == 'bottom' ? { top: pos.top + pos.height,   left: pos.left + pos.width / 2 - actualWidth / 2 } :
//           placement == 'top'    ? { top: pos.top - actualHeight, left: pos.left + pos.width / 2 - actualWidth / 2 } :
//           placement == 'left'   ? { top: pos.top + pos.height / 2 - actualHeight / 2, left: pos.left - actualWidth } :
//        /* placement == 'right' */ { top: pos.top + pos.height / 2 - actualHeight / 2, left: pos.left + pos.width }

//  }

//  Tooltip.prototype.getViewportAdjustedDelta = function (placement, pos, actualWidth, actualHeight) {
//    var delta = { top: 0, left: 0 }
//    if (!this.$viewport) return delta

//    var viewportPadding = this.options.viewport && this.options.viewport.padding || 0
//    var viewportDimensions = this.getPosition(this.$viewport)

//    if (/right|left/.test(placement)) {
//      var topEdgeOffset    = pos.top - viewportPadding - viewportDimensions.scroll
//      var bottomEdgeOffset = pos.top + viewportPadding - viewportDimensions.scroll + actualHeight
//      if (topEdgeOffset < viewportDimensions.top) { // top overflow
//        delta.top = viewportDimensions.top - topEdgeOffset
//      } else if (bottomEdgeOffset > viewportDimensions.top + viewportDimensions.height) { // bottom overflow
//        delta.top = viewportDimensions.top + viewportDimensions.height - bottomEdgeOffset
//      }
//    } else {
//      var leftEdgeOffset  = pos.left - viewportPadding
//      var rightEdgeOffset = pos.left + viewportPadding + actualWidth
//      if (leftEdgeOffset < viewportDimensions.left) { // left overflow
//        delta.left = viewportDimensions.left - leftEdgeOffset
//      } else if (rightEdgeOffset > viewportDimensions.right) { // right overflow
//        delta.left = viewportDimensions.left + viewportDimensions.width - rightEdgeOffset
//      }
//    }

//    return delta
//  }

//  Tooltip.prototype.getTitle = function () {
//    var title
//    var $e = this.$element
//    var o  = this.options

//    title = $e.attr('data-original-title')
//      || (typeof o.title == 'function' ? o.title.call($e[0]) :  o.title)

//    return title
//  }

//  Tooltip.prototype.getUID = function (prefix) {
//    do prefix += ~~(Math.random() * 1000000)
//    while (document.getElementById(prefix))
//    return prefix
//  }

//  Tooltip.prototype.tip = function () {
//    if (!this.$tip) {
//      this.$tip = $(this.options.template)
//      if (this.$tip.length != 1) {
//        throw new Error(this.type + ' `template` option must consist of exactly 1 top-level element!')
//      }
//    }
//    return this.$tip
//  }

//  Tooltip.prototype.arrow = function () {
//    return (this.$arrow = this.$arrow || this.tip().find('.tooltip-arrow'))
//  }

//  Tooltip.prototype.enable = function () {
//    this.enabled = true
//  }

//  Tooltip.prototype.disable = function () {
//    this.enabled = false
//  }

//  Tooltip.prototype.toggleEnabled = function () {
//    this.enabled = !this.enabled
//  }

//  Tooltip.prototype.toggle = function (e) {
//    var self = this
//    if (e) {
//      self = $(e.currentTarget).data('bs.' + this.type)
//      if (!self) {
//        self = new this.constructor(e.currentTarget, this.getDelegateOptions())
//        $(e.currentTarget).data('bs.' + this.type, self)
//      }
//    }

//    if (e) {
//      self.inState.click = !self.inState.click
//      if (self.isInStateTrue()) self.enter(self)
//      else self.leave(self)
//    } else {
//      self.tip().hasClass('in') ? self.leave(self) : self.enter(self)
//    }
//  }

//  Tooltip.prototype.destroy = function () {
//    var that = this
//    clearTimeout(this.timeout)
//    this.hide(function () {
//      that.$element.off('.' + that.type).removeData('bs.' + that.type)
//      if (that.$tip) {
//        that.$tip.detach()
//      }
//      that.$tip = null
//      that.$arrow = null
//      that.$viewport = null
//    })
//  }


//  // TOOLTIP PLUGIN DEFINITION
//  // =========================

//  function Plugin(option) {
//    return this.each(function () {
//      var $this   = $(this)
//      var data    = $this.data('bs.tooltip')
//      var options = typeof option == 'object' && option

//      if (!data && /destroy|hide/.test(option)) return
//      if (!data) $this.data('bs.tooltip', (data = new Tooltip(this, options)))
//      if (typeof option == 'string') data[option]()
//    })
//  }

//  var old = $.fn.tooltip

//  $.fn.tooltip             = Plugin
//  $.fn.tooltip.Constructor = Tooltip


//  // TOOLTIP NO CONFLICT
//  // ===================

//  $.fn.tooltip.noConflict = function () {
//    $.fn.tooltip = old
//    return this
//  }

//}(jQuery);

///* ========================================================================
// * Bootstrap: popover.js v3.3.6
// * http://getbootstrap.com/javascript/#popovers
// * ========================================================================
// * Copyright 2011-2015 Twitter, Inc.
// * Licensed under MIT (https://github.com/twbs/bootstrap/blob/master/LICENSE)
// * ======================================================================== */


//+function ($) {
//  'use strict';

//  // POPOVER PUBLIC CLASS DEFINITION
//  // ===============================

//  var Popover = function (element, options) {
//    this.init('popover', element, options)
//  }

//  if (!$.fn.tooltip) throw new Error('Popover requires tooltip.js')

//  Popover.VERSION  = '3.3.6'

//  Popover.DEFAULTS = $.extend({}, $.fn.tooltip.Constructor.DEFAULTS, {
//    placement: 'right',
//    trigger: 'click',
//    content: '',
//    template: '<div class="popover" role="tooltip"><div class="arrow"></div><h3 class="popover-title"></h3><div class="popover-content"></div></div>'
//  })


//  // NOTE: POPOVER EXTENDS tooltip.js
//  // ================================

//  Popover.prototype = $.extend({}, $.fn.tooltip.Constructor.prototype)

//  Popover.prototype.constructor = Popover

//  Popover.prototype.getDefaults = function () {
//    return Popover.DEFAULTS
//  }

//  Popover.prototype.setContent = function () {
//    var $tip    = this.tip()
//    var title   = this.getTitle()
//    var content = this.getContent()

//    $tip.find('.popover-title')[this.options.html ? 'html' : 'text'](title)
//    $tip.find('.popover-content').children().detach().end()[ // we use append for html objects to maintain js events
//      this.options.html ? (typeof content == 'string' ? 'html' : 'append') : 'text'
//    ](content)

//    $tip.removeClass('fade top bottom left right in')

//    // IE8 doesn't accept hiding via the `:empty` pseudo selector, we have to do
//    // this manually by checking the contents.
//    if (!$tip.find('.popover-title').html()) $tip.find('.popover-title').hide()
//  }

//  Popover.prototype.hasContent = function () {
//    return this.getTitle() || this.getContent()
//  }

//  Popover.prototype.getContent = function () {
//    var $e = this.$element
//    var o  = this.options

//    return $e.attr('data-content')
//      || (typeof o.content == 'function' ?
//            o.content.call($e[0]) :
//            o.content)
//  }

//  Popover.prototype.arrow = function () {
//    return (this.$arrow = this.$arrow || this.tip().find('.arrow'))
//  }


//  // POPOVER PLUGIN DEFINITION
//  // =========================

//  function Plugin(option) {
//    return this.each(function () {
//      var $this   = $(this)
//      var data    = $this.data('bs.popover')
//      var options = typeof option == 'object' && option

//      if (!data && /destroy|hide/.test(option)) return
//      if (!data) $this.data('bs.popover', (data = new Popover(this, options)))
//      if (typeof option == 'string') data[option]()
//    })
//  }

//  var old = $.fn.popover

//  $.fn.popover             = Plugin
//  $.fn.popover.Constructor = Popover


//  // POPOVER NO CONFLICT
//  // ===================

//  $.fn.popover.noConflict = function () {
//    $.fn.popover = old
//    return this
//  }

//}(jQuery);

///* ========================================================================
// * Bootstrap: scrollspy.js v3.3.6
// * http://getbootstrap.com/javascript/#scrollspy
// * ========================================================================
// * Copyright 2011-2015 Twitter, Inc.
// * Licensed under MIT (https://github.com/twbs/bootstrap/blob/master/LICENSE)
// * ======================================================================== */


//+function ($) {
//  'use strict';

//  // SCROLLSPY CLASS DEFINITION
//  // ==========================

//  function ScrollSpy(element, options) {
//    this.$body          = $(document.body)
//    this.$scrollElement = $(element).is(document.body) ? $(window) : $(element)
//    this.options        = $.extend({}, ScrollSpy.DEFAULTS, options)
//    this.selector       = (this.options.target || '') + ' .nav li > a'
//    this.offsets        = []
//    this.targets        = []
//    this.activeTarget   = null
//    this.scrollHeight   = 0

//    this.$scrollElement.on('scroll.bs.scrollspy', $.proxy(this.process, this))
//    this.refresh()
//    this.process()
//  }

//  ScrollSpy.VERSION  = '3.3.6'

//  ScrollSpy.DEFAULTS = {
//    offset: 10
//  }

//  ScrollSpy.prototype.getScrollHeight = function () {
//    return this.$scrollElement[0].scrollHeight || Math.max(this.$body[0].scrollHeight, document.documentElement.scrollHeight)
//  }

//  ScrollSpy.prototype.refresh = function () {
//    var that          = this
//    var offsetMethod  = 'offset'
//    var offsetBase    = 0

//    this.offsets      = []
//    this.targets      = []
//    this.scrollHeight = this.getScrollHeight()

//    if (!$.isWindow(this.$scrollElement[0])) {
//      offsetMethod = 'position'
//      offsetBase   = this.$scrollElement.scrollTop()
//    }

//    this.$body
//      .find(this.selector)
//      .map(function () {
//        var $el   = $(this)
//        var href  = $el.data('target') || $el.attr('href')
//        var $href = /^#./.test(href) && $(href)

//        return ($href
//          && $href.length
//          && $href.is(':visible')
//          && [[$href[offsetMethod]().top + offsetBase, href]]) || null
//      })
//      .sort(function (a, b) { return a[0] - b[0] })
//      .each(function () {
//        that.offsets.push(this[0])
//        that.targets.push(this[1])
//      })
//  }

//  ScrollSpy.prototype.process = function () {
//    var scrollTop    = this.$scrollElement.scrollTop() + this.options.offset
//    var scrollHeight = this.getScrollHeight()
//    var maxScroll    = this.options.offset + scrollHeight - this.$scrollElement.height()
//    var offsets      = this.offsets
//    var targets      = this.targets
//    var activeTarget = this.activeTarget
//    var i

//    if (this.scrollHeight != scrollHeight) {
//      this.refresh()
//    }

//    if (scrollTop >= maxScroll) {
//      return activeTarget != (i = targets[targets.length - 1]) && this.activate(i)
//    }

//    if (activeTarget && scrollTop < offsets[0]) {
//      this.activeTarget = null
//      return this.clear()
//    }

//    for (i = offsets.length; i--;) {
//      activeTarget != targets[i]
//        && scrollTop >= offsets[i]
//        && (offsets[i + 1] === undefined || scrollTop < offsets[i + 1])
//        && this.activate(targets[i])
//    }
//  }

//  ScrollSpy.prototype.activate = function (target) {
//    this.activeTarget = target

//    this.clear()

//    var selector = this.selector +
//      '[data-target="' + target + '"],' +
//      this.selector + '[href="' + target + '"]'

//    var active = $(selector)
//      .parents('li')
//      .addClass('active')

//    if (active.parent('.dropdown-menu').length) {
//      active = active
//        .closest('li.dropdown')
//        .addClass('active')
//    }

//    active.trigger('activate.bs.scrollspy')
//  }

//  ScrollSpy.prototype.clear = function () {
//    $(this.selector)
//      .parentsUntil(this.options.target, '.active')
//      .removeClass('active')
//  }


//  // SCROLLSPY PLUGIN DEFINITION
//  // ===========================

//  function Plugin(option) {
//    return this.each(function () {
//      var $this   = $(this)
//      var data    = $this.data('bs.scrollspy')
//      var options = typeof option == 'object' && option

//      if (!data) $this.data('bs.scrollspy', (data = new ScrollSpy(this, options)))
//      if (typeof option == 'string') data[option]()
//    })
//  }

//  var old = $.fn.scrollspy

//  $.fn.scrollspy             = Plugin
//  $.fn.scrollspy.Constructor = ScrollSpy


//  // SCROLLSPY NO CONFLICT
//  // =====================

//  $.fn.scrollspy.noConflict = function () {
//    $.fn.scrollspy = old
//    return this
//  }


//  // SCROLLSPY DATA-API
//  // ==================

//  $(window).on('load.bs.scrollspy.data-api', function () {
//    $('[data-spy="scroll"]').each(function () {
//      var $spy = $(this)
//      Plugin.call($spy, $spy.data())
//    })
//  })

//}(jQuery);

///* ========================================================================
// * Bootstrap: tab.js v3.3.6
// * http://getbootstrap.com/javascript/#tabs
// * ========================================================================
// * Copyright 2011-2015 Twitter, Inc.
// * Licensed under MIT (https://github.com/twbs/bootstrap/blob/master/LICENSE)
// * ======================================================================== */


//+function ($) {
//  'use strict';

//  // TAB CLASS DEFINITION
//  // ====================

//  var Tab = function (element) {
//    // jscs:disable requireDollarBeforejQueryAssignment
//    this.element = $(element)
//    // jscs:enable requireDollarBeforejQueryAssignment
//  }

//  Tab.VERSION = '3.3.6'

//  Tab.TRANSITION_DURATION = 150

//  Tab.prototype.show = function () {
//    var $this    = this.element
//    var $ul      = $this.closest('ul:not(.dropdown-menu)')
//    var selector = $this.data('target')

//    if (!selector) {
//      selector = $this.attr('href')
//      selector = selector && selector.replace(/.*(?=#[^\s]*$)/, '') // strip for ie7
//    }

//    if ($this.parent('li').hasClass('active')) return

//    var $previous = $ul.find('.active:last a')
//    var hideEvent = $.Event('hide.bs.tab', {
//      relatedTarget: $this[0]
//    })
//    var showEvent = $.Event('show.bs.tab', {
//      relatedTarget: $previous[0]
//    })

//    $previous.trigger(hideEvent)
//    $this.trigger(showEvent)

//    if (showEvent.isDefaultPrevented() || hideEvent.isDefaultPrevented()) return

//    var $target = $(selector)

//    this.activate($this.closest('li'), $ul)
//    this.activate($target, $target.parent(), function () {
//      $previous.trigger({
//        type: 'hidden.bs.tab',
//        relatedTarget: $this[0]
//      })
//      $this.trigger({
//        type: 'shown.bs.tab',
//        relatedTarget: $previous[0]
//      })
//    })
//  }

//  Tab.prototype.activate = function (element, container, callback) {
//    var $active    = container.find('> .active')
//    var transition = callback
//      && $.support.transition
//      && ($active.length && $active.hasClass('fade') || !!container.find('> .fade').length)

//    function next() {
//      $active
//        .removeClass('active')
//        .find('> .dropdown-menu > .active')
//          .removeClass('active')
//        .end()
//        .find('[data-toggle="tab"]')
//          .attr('aria-expanded', false)

//      element
//        .addClass('active')
//        .find('[data-toggle="tab"]')
//          .attr('aria-expanded', true)

//      if (transition) {
//        element[0].offsetWidth // reflow for transition
//        element.addClass('in')
//      } else {
//        element.removeClass('fade')
//      }

//      if (element.parent('.dropdown-menu').length) {
//        element
//          .closest('li.dropdown')
//            .addClass('active')
//          .end()
//          .find('[data-toggle="tab"]')
//            .attr('aria-expanded', true)
//      }

//      callback && callback()
//    }

//    $active.length && transition ?
//      $active
//        .one('bsTransitionEnd', next)
//        .emulateTransitionEnd(Tab.TRANSITION_DURATION) :
//      next()

//    $active.removeClass('in')
//  }


//  // TAB PLUGIN DEFINITION
//  // =====================

//  function Plugin(option) {
//    return this.each(function () {
//      var $this = $(this)
//      var data  = $this.data('bs.tab')

//      if (!data) $this.data('bs.tab', (data = new Tab(this)))
//      if (typeof option == 'string') data[option]()
//    })
//  }

//  var old = $.fn.tab

//  $.fn.tab             = Plugin
//  $.fn.tab.Constructor = Tab


//  // TAB NO CONFLICT
//  // ===============

//  $.fn.tab.noConflict = function () {
//    $.fn.tab = old
//    return this
//  }


//  // TAB DATA-API
//  // ============

//  var clickHandler = function (e) {
//    e.preventDefault()
//    Plugin.call($(this), 'show')
//  }

//  $(document)
//    .on('click.bs.tab.data-api', '[data-toggle="tab"]', clickHandler)
//    .on('click.bs.tab.data-api', '[data-toggle="pill"]', clickHandler)

//}(jQuery);

///* ========================================================================
// * Bootstrap: affix.js v3.3.6
// * http://getbootstrap.com/javascript/#affix
// * ========================================================================
// * Copyright 2011-2015 Twitter, Inc.
// * Licensed under MIT (https://github.com/twbs/bootstrap/blob/master/LICENSE)
// * ======================================================================== */


//+function ($) {
//  'use strict';

//  // AFFIX CLASS DEFINITION
//  // ======================

//  var Affix = function (element, options) {
//    this.options = $.extend({}, Affix.DEFAULTS, options)

//    this.$target = $(this.options.target)
//      .on('scroll.bs.affix.data-api', $.proxy(this.checkPosition, this))
//      .on('click.bs.affix.data-api',  $.proxy(this.checkPositionWithEventLoop, this))

//    this.$element     = $(element)
//    this.affixed      = null
//    this.unpin        = null
//    this.pinnedOffset = null

//    this.checkPosition()
//  }

//  Affix.VERSION  = '3.3.6'

//  Affix.RESET    = 'affix affix-top affix-bottom'

//  Affix.DEFAULTS = {
//    offset: 0,
//    target: window
//  }

//  Affix.prototype.getState = function (scrollHeight, height, offsetTop, offsetBottom) {
//    var scrollTop    = this.$target.scrollTop()
//    var position     = this.$element.offset()
//    var targetHeight = this.$target.height()

//    if (offsetTop != null && this.affixed == 'top') return scrollTop < offsetTop ? 'top' : false

//    if (this.affixed == 'bottom') {
//      if (offsetTop != null) return (scrollTop + this.unpin <= position.top) ? false : 'bottom'
//      return (scrollTop + targetHeight <= scrollHeight - offsetBottom) ? false : 'bottom'
//    }

//    var initializing   = this.affixed == null
//    var colliderTop    = initializing ? scrollTop : position.top
//    var colliderHeight = initializing ? targetHeight : height

//    if (offsetTop != null && scrollTop <= offsetTop) return 'top'
//    if (offsetBottom != null && (colliderTop + colliderHeight >= scrollHeight - offsetBottom)) return 'bottom'

//    return false
//  }

//  Affix.prototype.getPinnedOffset = function () {
//    if (this.pinnedOffset) return this.pinnedOffset
//    this.$element.removeClass(Affix.RESET).addClass('affix')
//    var scrollTop = this.$target.scrollTop()
//    var position  = this.$element.offset()
//    return (this.pinnedOffset = position.top - scrollTop)
//  }

//  Affix.prototype.checkPositionWithEventLoop = function () {
//    setTimeout($.proxy(this.checkPosition, this), 1)
//  }

//  Affix.prototype.checkPosition = function () {
//    if (!this.$element.is(':visible')) return

//    var height       = this.$element.height()
//    var offset       = this.options.offset
//    var offsetTop    = offset.top
//    var offsetBottom = offset.bottom
//    var scrollHeight = Math.max($(document).height(), $(document.body).height())

//    if (typeof offset != 'object')         offsetBottom = offsetTop = offset
//    if (typeof offsetTop == 'function')    offsetTop    = offset.top(this.$element)
//    if (typeof offsetBottom == 'function') offsetBottom = offset.bottom(this.$element)

//    var affix = this.getState(scrollHeight, height, offsetTop, offsetBottom)

//    if (this.affixed != affix) {
//      if (this.unpin != null) this.$element.css('top', '')

//      var affixType = 'affix' + (affix ? '-' + affix : '')
//      var e         = $.Event(affixType + '.bs.affix')

//      this.$element.trigger(e)

//      if (e.isDefaultPrevented()) return

//      this.affixed = affix
//      this.unpin = affix == 'bottom' ? this.getPinnedOffset() : null

//      this.$element
//        .removeClass(Affix.RESET)
//        .addClass(affixType)
//        .trigger(affixType.replace('affix', 'affixed') + '.bs.affix')
//    }

//    if (affix == 'bottom') {
//      this.$element.offset({
//        top: scrollHeight - height - offsetBottom
//      })
//    }
//  }


//  // AFFIX PLUGIN DEFINITION
//  // =======================

//  function Plugin(option) {
//    return this.each(function () {
//      var $this   = $(this)
//      var data    = $this.data('bs.affix')
//      var options = typeof option == 'object' && option

//      if (!data) $this.data('bs.affix', (data = new Affix(this, options)))
//      if (typeof option == 'string') data[option]()
//    })
//  }

//  var old = $.fn.affix

//  $.fn.affix             = Plugin
//  $.fn.affix.Constructor = Affix


//  // AFFIX NO CONFLICT
//  // =================

//  $.fn.affix.noConflict = function () {
//    $.fn.affix = old
//    return this
//  }


//  // AFFIX DATA-API
//  // ==============

//  $(window).on('load', function () {
//    $('[data-spy="affix"]').each(function () {
//      var $spy = $(this)
//      var data = $spy.data()

//      data.offset = data.offset || {}

//      if (data.offsetBottom != null) data.offset.bottom = data.offsetBottom
//      if (data.offsetTop    != null) data.offset.top    = data.offsetTop

//      Plugin.call($spy, data)
//    })
//  })

//}(jQuery);


/*!
 * Bootstrap v3.4.1 (https://getbootstrap.com/)
 * Copyright 2011-2019 Twitter, Inc.
 * Licensed under the MIT license
 */
if ("undefined" == typeof jQuery) throw new Error("Bootstrap's JavaScript requires jQuery"); !function (t) { "use strict"; var e = jQuery.fn.jquery.split(" ")[0].split("."); if (e[0] < 2 && e[1] < 9 || 1 == e[0] && 9 == e[1] && e[2] < 1 || 3 < e[0]) throw new Error("Bootstrap's JavaScript requires jQuery version 1.9.1 or higher, but lower than version 4") }(), function (n) { "use strict"; n.fn.emulateTransitionEnd = function (t) { var e = !1, i = this; n(this).one("bsTransitionEnd", function () { e = !0 }); return setTimeout(function () { e || n(i).trigger(n.support.transition.end) }, t), this }, n(function () { n.support.transition = function o() { var t = document.createElement("bootstrap"), e = { WebkitTransition: "webkitTransitionEnd", MozTransition: "transitionend", OTransition: "oTransitionEnd otransitionend", transition: "transitionend" }; for (var i in e) if (t.style[i] !== undefined) return { end: e[i] }; return !1 }(), n.support.transition && (n.event.special.bsTransitionEnd = { bindType: n.support.transition.end, delegateType: n.support.transition.end, handle: function (t) { if (n(t.target).is(this)) return t.handleObj.handler.apply(this, arguments) } }) }) }(jQuery), function (s) { "use strict"; var e = '[data-dismiss="alert"]', a = function (t) { s(t).on("click", e, this.close) }; a.VERSION = "3.4.1", a.TRANSITION_DURATION = 150, a.prototype.close = function (t) { var e = s(this), i = e.attr("data-target"); i || (i = (i = e.attr("href")) && i.replace(/.*(?=#[^\s]*$)/, "")), i = "#" === i ? [] : i; var o = s(document).find(i); function n() { o.detach().trigger("closed.bs.alert").remove() } t && t.preventDefault(), o.length || (o = e.closest(".alert")), o.trigger(t = s.Event("close.bs.alert")), t.isDefaultPrevented() || (o.removeClass("in"), s.support.transition && o.hasClass("fade") ? o.one("bsTransitionEnd", n).emulateTransitionEnd(a.TRANSITION_DURATION) : n()) }; var t = s.fn.alert; s.fn.alert = function o(i) { return this.each(function () { var t = s(this), e = t.data("bs.alert"); e || t.data("bs.alert", e = new a(this)), "string" == typeof i && e[i].call(t) }) }, s.fn.alert.Constructor = a, s.fn.alert.noConflict = function () { return s.fn.alert = t, this }, s(document).on("click.bs.alert.data-api", e, a.prototype.close) }(jQuery), function (s) { "use strict"; var n = function (t, e) { this.$element = s(t), this.options = s.extend({}, n.DEFAULTS, e), this.isLoading = !1 }; function i(o) { return this.each(function () { var t = s(this), e = t.data("bs.button"), i = "object" == typeof o && o; e || t.data("bs.button", e = new n(this, i)), "toggle" == o ? e.toggle() : o && e.setState(o) }) } n.VERSION = "3.4.1", n.DEFAULTS = { loadingText: "loading..." }, n.prototype.setState = function (t) { var e = "disabled", i = this.$element, o = i.is("input") ? "val" : "html", n = i.data(); t += "Text", null == n.resetText && i.data("resetText", i[o]()), setTimeout(s.proxy(function () { i[o](null == n[t] ? this.options[t] : n[t]), "loadingText" == t ? (this.isLoading = !0, i.addClass(e).attr(e, e).prop(e, !0)) : this.isLoading && (this.isLoading = !1, i.removeClass(e).removeAttr(e).prop(e, !1)) }, this), 0) }, n.prototype.toggle = function () { var t = !0, e = this.$element.closest('[data-toggle="buttons"]'); if (e.length) { var i = this.$element.find("input"); "radio" == i.prop("type") ? (i.prop("checked") && (t = !1), e.find(".active").removeClass("active"), this.$element.addClass("active")) : "checkbox" == i.prop("type") && (i.prop("checked") !== this.$element.hasClass("active") && (t = !1), this.$element.toggleClass("active")), i.prop("checked", this.$element.hasClass("active")), t && i.trigger("change") } else this.$element.attr("aria-pressed", !this.$element.hasClass("active")), this.$element.toggleClass("active") }; var t = s.fn.button; s.fn.button = i, s.fn.button.Constructor = n, s.fn.button.noConflict = function () { return s.fn.button = t, this }, s(document).on("click.bs.button.data-api", '[data-toggle^="button"]', function (t) { var e = s(t.target).closest(".btn"); i.call(e, "toggle"), s(t.target).is('input[type="radio"], input[type="checkbox"]') || (t.preventDefault(), e.is("input,button") ? e.trigger("focus") : e.find("input:visible,button:visible").first().trigger("focus")) }).on("focus.bs.button.data-api blur.bs.button.data-api", '[data-toggle^="button"]', function (t) { s(t.target).closest(".btn").toggleClass("focus", /^focus(in)?$/.test(t.type)) }) }(jQuery), function (p) { "use strict"; var c = function (t, e) { this.$element = p(t), this.$indicators = this.$element.find(".carousel-indicators"), this.options = e, this.paused = null, this.sliding = null, this.interval = null, this.$active = null, this.$items = null, this.options.keyboard && this.$element.on("keydown.bs.carousel", p.proxy(this.keydown, this)), "hover" == this.options.pause && !("ontouchstart" in document.documentElement) && this.$element.on("mouseenter.bs.carousel", p.proxy(this.pause, this)).on("mouseleave.bs.carousel", p.proxy(this.cycle, this)) }; function r(n) { return this.each(function () { var t = p(this), e = t.data("bs.carousel"), i = p.extend({}, c.DEFAULTS, t.data(), "object" == typeof n && n), o = "string" == typeof n ? n : i.slide; e || t.data("bs.carousel", e = new c(this, i)), "number" == typeof n ? e.to(n) : o ? e[o]() : i.interval && e.pause().cycle() }) } c.VERSION = "3.4.1", c.TRANSITION_DURATION = 600, c.DEFAULTS = { interval: 5e3, pause: "hover", wrap: !0, keyboard: !0 }, c.prototype.keydown = function (t) { if (!/input|textarea/i.test(t.target.tagName)) { switch (t.which) { case 37: this.prev(); break; case 39: this.next(); break; default: return }t.preventDefault() } }, c.prototype.cycle = function (t) { return t || (this.paused = !1), this.interval && clearInterval(this.interval), this.options.interval && !this.paused && (this.interval = setInterval(p.proxy(this.next, this), this.options.interval)), this }, c.prototype.getItemIndex = function (t) { return this.$items = t.parent().children(".item"), this.$items.index(t || this.$active) }, c.prototype.getItemForDirection = function (t, e) { var i = this.getItemIndex(e); if (("prev" == t && 0 === i || "next" == t && i == this.$items.length - 1) && !this.options.wrap) return e; var o = (i + ("prev" == t ? -1 : 1)) % this.$items.length; return this.$items.eq(o) }, c.prototype.to = function (t) { var e = this, i = this.getItemIndex(this.$active = this.$element.find(".item.active")); if (!(t > this.$items.length - 1 || t < 0)) return this.sliding ? this.$element.one("slid.bs.carousel", function () { e.to(t) }) : i == t ? this.pause().cycle() : this.slide(i < t ? "next" : "prev", this.$items.eq(t)) }, c.prototype.pause = function (t) { return t || (this.paused = !0), this.$element.find(".next, .prev").length && p.support.transition && (this.$element.trigger(p.support.transition.end), this.cycle(!0)), this.interval = clearInterval(this.interval), this }, c.prototype.next = function () { if (!this.sliding) return this.slide("next") }, c.prototype.prev = function () { if (!this.sliding) return this.slide("prev") }, c.prototype.slide = function (t, e) { var i = this.$element.find(".item.active"), o = e || this.getItemForDirection(t, i), n = this.interval, s = "next" == t ? "left" : "right", a = this; if (o.hasClass("active")) return this.sliding = !1; var r = o[0], l = p.Event("slide.bs.carousel", { relatedTarget: r, direction: s }); if (this.$element.trigger(l), !l.isDefaultPrevented()) { if (this.sliding = !0, n && this.pause(), this.$indicators.length) { this.$indicators.find(".active").removeClass("active"); var h = p(this.$indicators.children()[this.getItemIndex(o)]); h && h.addClass("active") } var d = p.Event("slid.bs.carousel", { relatedTarget: r, direction: s }); return p.support.transition && this.$element.hasClass("slide") ? (o.addClass(t), "object" == typeof o && o.length && o[0].offsetWidth, i.addClass(s), o.addClass(s), i.one("bsTransitionEnd", function () { o.removeClass([t, s].join(" ")).addClass("active"), i.removeClass(["active", s].join(" ")), a.sliding = !1, setTimeout(function () { a.$element.trigger(d) }, 0) }).emulateTransitionEnd(c.TRANSITION_DURATION)) : (i.removeClass("active"), o.addClass("active"), this.sliding = !1, this.$element.trigger(d)), n && this.cycle(), this } }; var t = p.fn.carousel; p.fn.carousel = r, p.fn.carousel.Constructor = c, p.fn.carousel.noConflict = function () { return p.fn.carousel = t, this }; var e = function (t) { var e = p(this), i = e.attr("href"); i && (i = i.replace(/.*(?=#[^\s]+$)/, "")); var o = e.attr("data-target") || i, n = p(document).find(o); if (n.hasClass("carousel")) { var s = p.extend({}, n.data(), e.data()), a = e.attr("data-slide-to"); a && (s.interval = !1), r.call(n, s), a && n.data("bs.carousel").to(a), t.preventDefault() } }; p(document).on("click.bs.carousel.data-api", "[data-slide]", e).on("click.bs.carousel.data-api", "[data-slide-to]", e), p(window).on("load", function () { p('[data-ride="carousel"]').each(function () { var t = p(this); r.call(t, t.data()) }) }) }(jQuery), function (a) { "use strict"; var r = function (t, e) { this.$element = a(t), this.options = a.extend({}, r.DEFAULTS, e), this.$trigger = a('[data-toggle="collapse"][href="#' + t.id + '"],[data-toggle="collapse"][data-target="#' + t.id + '"]'), this.transitioning = null, this.options.parent ? this.$parent = this.getParent() : this.addAriaAndCollapsedClass(this.$element, this.$trigger), this.options.toggle && this.toggle() }; function n(t) { var e, i = t.attr("data-target") || (e = t.attr("href")) && e.replace(/.*(?=#[^\s]+$)/, ""); return a(document).find(i) } function l(o) { return this.each(function () { var t = a(this), e = t.data("bs.collapse"), i = a.extend({}, r.DEFAULTS, t.data(), "object" == typeof o && o); !e && i.toggle && /show|hide/.test(o) && (i.toggle = !1), e || t.data("bs.collapse", e = new r(this, i)), "string" == typeof o && e[o]() }) } r.VERSION = "3.4.1", r.TRANSITION_DURATION = 350, r.DEFAULTS = { toggle: !0 }, r.prototype.dimension = function () { return this.$element.hasClass("width") ? "width" : "height" }, r.prototype.show = function () { if (!this.transitioning && !this.$element.hasClass("in")) { var t, e = this.$parent && this.$parent.children(".panel").children(".in, .collapsing"); if (!(e && e.length && (t = e.data("bs.collapse")) && t.transitioning)) { var i = a.Event("show.bs.collapse"); if (this.$element.trigger(i), !i.isDefaultPrevented()) { e && e.length && (l.call(e, "hide"), t || e.data("bs.collapse", null)); var o = this.dimension(); this.$element.removeClass("collapse").addClass("collapsing")[o](0).attr("aria-expanded", !0), this.$trigger.removeClass("collapsed").attr("aria-expanded", !0), this.transitioning = 1; var n = function () { this.$element.removeClass("collapsing").addClass("collapse in")[o](""), this.transitioning = 0, this.$element.trigger("shown.bs.collapse") }; if (!a.support.transition) return n.call(this); var s = a.camelCase(["scroll", o].join("-")); this.$element.one("bsTransitionEnd", a.proxy(n, this)).emulateTransitionEnd(r.TRANSITION_DURATION)[o](this.$element[0][s]) } } } }, r.prototype.hide = function () { if (!this.transitioning && this.$element.hasClass("in")) { var t = a.Event("hide.bs.collapse"); if (this.$element.trigger(t), !t.isDefaultPrevented()) { var e = this.dimension(); this.$element[e](this.$element[e]())[0].offsetHeight, this.$element.addClass("collapsing").removeClass("collapse in").attr("aria-expanded", !1), this.$trigger.addClass("collapsed").attr("aria-expanded", !1), this.transitioning = 1; var i = function () { this.transitioning = 0, this.$element.removeClass("collapsing").addClass("collapse").trigger("hidden.bs.collapse") }; if (!a.support.transition) return i.call(this); this.$element[e](0).one("bsTransitionEnd", a.proxy(i, this)).emulateTransitionEnd(r.TRANSITION_DURATION) } } }, r.prototype.toggle = function () { this[this.$element.hasClass("in") ? "hide" : "show"]() }, r.prototype.getParent = function () { return a(document).find(this.options.parent).find('[data-toggle="collapse"][data-parent="' + this.options.parent + '"]').each(a.proxy(function (t, e) { var i = a(e); this.addAriaAndCollapsedClass(n(i), i) }, this)).end() }, r.prototype.addAriaAndCollapsedClass = function (t, e) { var i = t.hasClass("in"); t.attr("aria-expanded", i), e.toggleClass("collapsed", !i).attr("aria-expanded", i) }; var t = a.fn.collapse; a.fn.collapse = l, a.fn.collapse.Constructor = r, a.fn.collapse.noConflict = function () { return a.fn.collapse = t, this }, a(document).on("click.bs.collapse.data-api", '[data-toggle="collapse"]', function (t) { var e = a(this); e.attr("data-target") || t.preventDefault(); var i = n(e), o = i.data("bs.collapse") ? "toggle" : e.data(); l.call(i, o) }) }(jQuery), function (a) { "use strict"; var r = '[data-toggle="dropdown"]', o = function (t) { a(t).on("click.bs.dropdown", this.toggle) }; function l(t) { var e = t.attr("data-target"); e || (e = (e = t.attr("href")) && /#[A-Za-z]/.test(e) && e.replace(/.*(?=#[^\s]*$)/, "")); var i = "#" !== e ? a(document).find(e) : null; return i && i.length ? i : t.parent() } function s(o) { o && 3 === o.which || (a(".dropdown-backdrop").remove(), a(r).each(function () { var t = a(this), e = l(t), i = { relatedTarget: this }; e.hasClass("open") && (o && "click" == o.type && /input|textarea/i.test(o.target.tagName) && a.contains(e[0], o.target) || (e.trigger(o = a.Event("hide.bs.dropdown", i)), o.isDefaultPrevented() || (t.attr("aria-expanded", "false"), e.removeClass("open").trigger(a.Event("hidden.bs.dropdown", i))))) })) } o.VERSION = "3.4.1", o.prototype.toggle = function (t) { var e = a(this); if (!e.is(".disabled, :disabled")) { var i = l(e), o = i.hasClass("open"); if (s(), !o) { "ontouchstart" in document.documentElement && !i.closest(".navbar-nav").length && a(document.createElement("div")).addClass("dropdown-backdrop").insertAfter(a(this)).on("click", s); var n = { relatedTarget: this }; if (i.trigger(t = a.Event("show.bs.dropdown", n)), t.isDefaultPrevented()) return; e.trigger("focus").attr("aria-expanded", "true"), i.toggleClass("open").trigger(a.Event("shown.bs.dropdown", n)) } return !1 } }, o.prototype.keydown = function (t) { if (/(38|40|27|32)/.test(t.which) && !/input|textarea/i.test(t.target.tagName)) { var e = a(this); if (t.preventDefault(), t.stopPropagation(), !e.is(".disabled, :disabled")) { var i = l(e), o = i.hasClass("open"); if (!o && 27 != t.which || o && 27 == t.which) return 27 == t.which && i.find(r).trigger("focus"), e.trigger("click"); var n = i.find(".dropdown-menu li:not(.disabled):visible a"); if (n.length) { var s = n.index(t.target); 38 == t.which && 0 < s && s-- , 40 == t.which && s < n.length - 1 && s++ , ~s || (s = 0), n.eq(s).trigger("focus") } } } }; var t = a.fn.dropdown; a.fn.dropdown = function e(i) { return this.each(function () { var t = a(this), e = t.data("bs.dropdown"); e || t.data("bs.dropdown", e = new o(this)), "string" == typeof i && e[i].call(t) }) }, a.fn.dropdown.Constructor = o, a.fn.dropdown.noConflict = function () { return a.fn.dropdown = t, this }, a(document).on("click.bs.dropdown.data-api", s).on("click.bs.dropdown.data-api", ".dropdown form", function (t) { t.stopPropagation() }).on("click.bs.dropdown.data-api", r, o.prototype.toggle).on("keydown.bs.dropdown.data-api", r, o.prototype.keydown).on("keydown.bs.dropdown.data-api", ".dropdown-menu", o.prototype.keydown) }(jQuery), function (a) { "use strict"; var s = function (t, e) { this.options = e, this.$body = a(document.body), this.$element = a(t), this.$dialog = this.$element.find(".modal-dialog"), this.$backdrop = null, this.isShown = null, this.originalBodyPad = null, this.scrollbarWidth = 0, this.ignoreBackdropClick = !1, this.fixedContent = ".navbar-fixed-top, .navbar-fixed-bottom", this.options.remote && this.$element.find(".modal-content").load(this.options.remote, a.proxy(function () { this.$element.trigger("loaded.bs.modal") }, this)) }; function r(o, n) { return this.each(function () { var t = a(this), e = t.data("bs.modal"), i = a.extend({}, s.DEFAULTS, t.data(), "object" == typeof o && o); e || t.data("bs.modal", e = new s(this, i)), "string" == typeof o ? e[o](n) : i.show && e.show(n) }) } s.VERSION = "3.4.1", s.TRANSITION_DURATION = 300, s.BACKDROP_TRANSITION_DURATION = 150, s.DEFAULTS = { backdrop: !0, keyboard: !0, show: !0 }, s.prototype.toggle = function (t) { return this.isShown ? this.hide() : this.show(t) }, s.prototype.show = function (i) { var o = this, t = a.Event("show.bs.modal", { relatedTarget: i }); this.$element.trigger(t), this.isShown || t.isDefaultPrevented() || (this.isShown = !0, this.checkScrollbar(), this.setScrollbar(), this.$body.addClass("modal-open"), this.escape(), this.resize(), this.$element.on("click.dismiss.bs.modal", '[data-dismiss="modal"]', a.proxy(this.hide, this)), this.$dialog.on("mousedown.dismiss.bs.modal", function () { o.$element.one("mouseup.dismiss.bs.modal", function (t) { a(t.target).is(o.$element) && (o.ignoreBackdropClick = !0) }) }), this.backdrop(function () { var t = a.support.transition && o.$element.hasClass("fade"); o.$element.parent().length || o.$element.appendTo(o.$body), o.$element.show().scrollTop(0), o.adjustDialog(), t && o.$element[0].offsetWidth, o.$element.addClass("in"), o.enforceFocus(); var e = a.Event("shown.bs.modal", { relatedTarget: i }); t ? o.$dialog.one("bsTransitionEnd", function () { o.$element.trigger("focus").trigger(e) }).emulateTransitionEnd(s.TRANSITION_DURATION) : o.$element.trigger("focus").trigger(e) })) }, s.prototype.hide = function (t) { t && t.preventDefault(), t = a.Event("hide.bs.modal"), this.$element.trigger(t), this.isShown && !t.isDefaultPrevented() && (this.isShown = !1, this.escape(), this.resize(), a(document).off("focusin.bs.modal"), this.$element.removeClass("in").off("click.dismiss.bs.modal").off("mouseup.dismiss.bs.modal"), this.$dialog.off("mousedown.dismiss.bs.modal"), a.support.transition && this.$element.hasClass("fade") ? this.$element.one("bsTransitionEnd", a.proxy(this.hideModal, this)).emulateTransitionEnd(s.TRANSITION_DURATION) : this.hideModal()) }, s.prototype.enforceFocus = function () { a(document).off("focusin.bs.modal").on("focusin.bs.modal", a.proxy(function (t) { document === t.target || this.$element[0] === t.target || this.$element.has(t.target).length || this.$element.trigger("focus") }, this)) }, s.prototype.escape = function () { this.isShown && this.options.keyboard ? this.$element.on("keydown.dismiss.bs.modal", a.proxy(function (t) { 27 == t.which && this.hide() }, this)) : this.isShown || this.$element.off("keydown.dismiss.bs.modal") }, s.prototype.resize = function () { this.isShown ? a(window).on("resize.bs.modal", a.proxy(this.handleUpdate, this)) : a(window).off("resize.bs.modal") }, s.prototype.hideModal = function () { var t = this; this.$element.hide(), this.backdrop(function () { t.$body.removeClass("modal-open"), t.resetAdjustments(), t.resetScrollbar(), t.$element.trigger("hidden.bs.modal") }) }, s.prototype.removeBackdrop = function () { this.$backdrop && this.$backdrop.remove(), this.$backdrop = null }, s.prototype.backdrop = function (t) { var e = this, i = this.$element.hasClass("fade") ? "fade" : ""; if (this.isShown && this.options.backdrop) { var o = a.support.transition && i; if (this.$backdrop = a(document.createElement("div")).addClass("modal-backdrop " + i).appendTo(this.$body), this.$element.on("click.dismiss.bs.modal", a.proxy(function (t) { this.ignoreBackdropClick ? this.ignoreBackdropClick = !1 : t.target === t.currentTarget && ("static" == this.options.backdrop ? this.$element[0].focus() : this.hide()) }, this)), o && this.$backdrop[0].offsetWidth, this.$backdrop.addClass("in"), !t) return; o ? this.$backdrop.one("bsTransitionEnd", t).emulateTransitionEnd(s.BACKDROP_TRANSITION_DURATION) : t() } else if (!this.isShown && this.$backdrop) { this.$backdrop.removeClass("in"); var n = function () { e.removeBackdrop(), t && t() }; a.support.transition && this.$element.hasClass("fade") ? this.$backdrop.one("bsTransitionEnd", n).emulateTransitionEnd(s.BACKDROP_TRANSITION_DURATION) : n() } else t && t() }, s.prototype.handleUpdate = function () { this.adjustDialog() }, s.prototype.adjustDialog = function () { var t = this.$element[0].scrollHeight > document.documentElement.clientHeight; this.$element.css({ paddingLeft: !this.bodyIsOverflowing && t ? this.scrollbarWidth : "", paddingRight: this.bodyIsOverflowing && !t ? this.scrollbarWidth : "" }) }, s.prototype.resetAdjustments = function () { this.$element.css({ paddingLeft: "", paddingRight: "" }) }, s.prototype.checkScrollbar = function () { var t = window.innerWidth; if (!t) { var e = document.documentElement.getBoundingClientRect(); t = e.right - Math.abs(e.left) } this.bodyIsOverflowing = document.body.clientWidth < t, this.scrollbarWidth = this.measureScrollbar() }, s.prototype.setScrollbar = function () { var t = parseInt(this.$body.css("padding-right") || 0, 10); this.originalBodyPad = document.body.style.paddingRight || ""; var n = this.scrollbarWidth; this.bodyIsOverflowing && (this.$body.css("padding-right", t + n), a(this.fixedContent).each(function (t, e) { var i = e.style.paddingRight, o = a(e).css("padding-right"); a(e).data("padding-right", i).css("padding-right", parseFloat(o) + n + "px") })) }, s.prototype.resetScrollbar = function () { this.$body.css("padding-right", this.originalBodyPad), a(this.fixedContent).each(function (t, e) { var i = a(e).data("padding-right"); a(e).removeData("padding-right"), e.style.paddingRight = i || "" }) }, s.prototype.measureScrollbar = function () { var t = document.createElement("div"); t.className = "modal-scrollbar-measure", this.$body.append(t); var e = t.offsetWidth - t.clientWidth; return this.$body[0].removeChild(t), e }; var t = a.fn.modal; a.fn.modal = r, a.fn.modal.Constructor = s, a.fn.modal.noConflict = function () { return a.fn.modal = t, this }, a(document).on("click.bs.modal.data-api", '[data-toggle="modal"]', function (t) { var e = a(this), i = e.attr("href"), o = e.attr("data-target") || i && i.replace(/.*(?=#[^\s]+$)/, ""), n = a(document).find(o), s = n.data("bs.modal") ? "toggle" : a.extend({ remote: !/#/.test(i) && i }, n.data(), e.data()); e.is("a") && t.preventDefault(), n.one("show.bs.modal", function (t) { t.isDefaultPrevented() || n.one("hidden.bs.modal", function () { e.is(":visible") && e.trigger("focus") }) }), r.call(n, s, this) }) }(jQuery), function (g) { "use strict"; var o = ["sanitize", "whiteList", "sanitizeFn"], a = ["background", "cite", "href", "itemtype", "longdesc", "poster", "src", "xlink:href"], t = { "*": ["class", "dir", "id", "lang", "role", /^aria-[\w-]*$/i], a: ["target", "href", "title", "rel"], area: [], b: [], br: [], col: [], code: [], div: [], em: [], hr: [], h1: [], h2: [], h3: [], h4: [], h5: [], h6: [], i: [], img: ["src", "alt", "title", "width", "height"], li: [], ol: [], p: [], pre: [], s: [], small: [], span: [], sub: [], sup: [], strong: [], u: [], ul: [] }, r = /^(?:(?:https?|mailto|ftp|tel|file):|[^&:/?#]*(?:[/?#]|$))/gi, l = /^data:(?:image\/(?:bmp|gif|jpeg|jpg|png|tiff|webp)|video\/(?:mpeg|mp4|ogg|webm)|audio\/(?:mp3|oga|ogg|opus));base64,[a-z0-9+/]+=*$/i; function u(t, e) { var i = t.nodeName.toLowerCase(); if (-1 !== g.inArray(i, e)) return -1 === g.inArray(i, a) || Boolean(t.nodeValue.match(r) || t.nodeValue.match(l)); for (var o = g(e).filter(function (t, e) { return e instanceof RegExp }), n = 0, s = o.length; n < s; n++)if (i.match(o[n])) return !0; return !1 } function n(t, e, i) { if (0 === t.length) return t; if (i && "function" == typeof i) return i(t); if (!document.implementation || !document.implementation.createHTMLDocument) return t; var o = document.implementation.createHTMLDocument("sanitization"); o.body.innerHTML = t; for (var n = g.map(e, function (t, e) { return e }), s = g(o.body).find("*"), a = 0, r = s.length; a < r; a++) { var l = s[a], h = l.nodeName.toLowerCase(); if (-1 !== g.inArray(h, n)) for (var d = g.map(l.attributes, function (t) { return t }), p = [].concat(e["*"] || [], e[h] || []), c = 0, f = d.length; c < f; c++)u(d[c], p) || l.removeAttribute(d[c].nodeName); else l.parentNode.removeChild(l) } return o.body.innerHTML } var m = function (t, e) { this.type = null, this.options = null, this.enabled = null, this.timeout = null, this.hoverState = null, this.$element = null, this.inState = null, this.init("tooltip", t, e) }; m.VERSION = "3.4.1", m.TRANSITION_DURATION = 150, m.DEFAULTS = { animation: !0, placement: "top", selector: !1, template: '<div class="tooltip" role="tooltip"><div class="tooltip-arrow"></div><div class="tooltip-inner"></div></div>', trigger: "hover focus", title: "", delay: 0, html: !1, container: !1, viewport: { selector: "body", padding: 0 }, sanitize: !0, sanitizeFn: null, whiteList: t }, m.prototype.init = function (t, e, i) { if (this.enabled = !0, this.type = t, this.$element = g(e), this.options = this.getOptions(i), this.$viewport = this.options.viewport && g(document).find(g.isFunction(this.options.viewport) ? this.options.viewport.call(this, this.$element) : this.options.viewport.selector || this.options.viewport), this.inState = { click: !1, hover: !1, focus: !1 }, this.$element[0] instanceof document.constructor && !this.options.selector) throw new Error("`selector` option must be specified when initializing " + this.type + " on the window.document object!"); for (var o = this.options.trigger.split(" "), n = o.length; n--;) { var s = o[n]; if ("click" == s) this.$element.on("click." + this.type, this.options.selector, g.proxy(this.toggle, this)); else if ("manual" != s) { var a = "hover" == s ? "mouseenter" : "focusin", r = "hover" == s ? "mouseleave" : "focusout"; this.$element.on(a + "." + this.type, this.options.selector, g.proxy(this.enter, this)), this.$element.on(r + "." + this.type, this.options.selector, g.proxy(this.leave, this)) } } this.options.selector ? this._options = g.extend({}, this.options, { trigger: "manual", selector: "" }) : this.fixTitle() }, m.prototype.getDefaults = function () { return m.DEFAULTS }, m.prototype.getOptions = function (t) { var e = this.$element.data(); for (var i in e) e.hasOwnProperty(i) && -1 !== g.inArray(i, o) && delete e[i]; return (t = g.extend({}, this.getDefaults(), e, t)).delay && "number" == typeof t.delay && (t.delay = { show: t.delay, hide: t.delay }), t.sanitize && (t.template = n(t.template, t.whiteList, t.sanitizeFn)), t }, m.prototype.getDelegateOptions = function () { var i = {}, o = this.getDefaults(); return this._options && g.each(this._options, function (t, e) { o[t] != e && (i[t] = e) }), i }, m.prototype.enter = function (t) { var e = t instanceof this.constructor ? t : g(t.currentTarget).data("bs." + this.type); if (e || (e = new this.constructor(t.currentTarget, this.getDelegateOptions()), g(t.currentTarget).data("bs." + this.type, e)), t instanceof g.Event && (e.inState["focusin" == t.type ? "focus" : "hover"] = !0), e.tip().hasClass("in") || "in" == e.hoverState) e.hoverState = "in"; else { if (clearTimeout(e.timeout), e.hoverState = "in", !e.options.delay || !e.options.delay.show) return e.show(); e.timeout = setTimeout(function () { "in" == e.hoverState && e.show() }, e.options.delay.show) } }, m.prototype.isInStateTrue = function () { for (var t in this.inState) if (this.inState[t]) return !0; return !1 }, m.prototype.leave = function (t) { var e = t instanceof this.constructor ? t : g(t.currentTarget).data("bs." + this.type); if (e || (e = new this.constructor(t.currentTarget, this.getDelegateOptions()), g(t.currentTarget).data("bs." + this.type, e)), t instanceof g.Event && (e.inState["focusout" == t.type ? "focus" : "hover"] = !1), !e.isInStateTrue()) { if (clearTimeout(e.timeout), e.hoverState = "out", !e.options.delay || !e.options.delay.hide) return e.hide(); e.timeout = setTimeout(function () { "out" == e.hoverState && e.hide() }, e.options.delay.hide) } }, m.prototype.show = function () { var t = g.Event("show.bs." + this.type); if (this.hasContent() && this.enabled) { this.$element.trigger(t); var e = g.contains(this.$element[0].ownerDocument.documentElement, this.$element[0]); if (t.isDefaultPrevented() || !e) return; var i = this, o = this.tip(), n = this.getUID(this.type); this.setContent(), o.attr("id", n), this.$element.attr("aria-describedby", n), this.options.animation && o.addClass("fade"); var s = "function" == typeof this.options.placement ? this.options.placement.call(this, o[0], this.$element[0]) : this.options.placement, a = /\s?auto?\s?/i, r = a.test(s); r && (s = s.replace(a, "") || "top"), o.detach().css({ top: 0, left: 0, display: "block" }).addClass(s).data("bs." + this.type, this), this.options.container ? o.appendTo(g(document).find(this.options.container)) : o.insertAfter(this.$element), this.$element.trigger("inserted.bs." + this.type); var l = this.getPosition(), h = o[0].offsetWidth, d = o[0].offsetHeight; if (r) { var p = s, c = this.getPosition(this.$viewport); s = "bottom" == s && l.bottom + d > c.bottom ? "top" : "top" == s && l.top - d < c.top ? "bottom" : "right" == s && l.right + h > c.width ? "left" : "left" == s && l.left - h < c.left ? "right" : s, o.removeClass(p).addClass(s) } var f = this.getCalculatedOffset(s, l, h, d); this.applyPlacement(f, s); var u = function () { var t = i.hoverState; i.$element.trigger("shown.bs." + i.type), i.hoverState = null, "out" == t && i.leave(i) }; g.support.transition && this.$tip.hasClass("fade") ? o.one("bsTransitionEnd", u).emulateTransitionEnd(m.TRANSITION_DURATION) : u() } }, m.prototype.applyPlacement = function (t, e) { var i = this.tip(), o = i[0].offsetWidth, n = i[0].offsetHeight, s = parseInt(i.css("margin-top"), 10), a = parseInt(i.css("margin-left"), 10); isNaN(s) && (s = 0), isNaN(a) && (a = 0), t.top += s, t.left += a, g.offset.setOffset(i[0], g.extend({ using: function (t) { i.css({ top: Math.round(t.top), left: Math.round(t.left) }) } }, t), 0), i.addClass("in"); var r = i[0].offsetWidth, l = i[0].offsetHeight; "top" == e && l != n && (t.top = t.top + n - l); var h = this.getViewportAdjustedDelta(e, t, r, l); h.left ? t.left += h.left : t.top += h.top; var d = /top|bottom/.test(e), p = d ? 2 * h.left - o + r : 2 * h.top - n + l, c = d ? "offsetWidth" : "offsetHeight"; i.offset(t), this.replaceArrow(p, i[0][c], d) }, m.prototype.replaceArrow = function (t, e, i) { this.arrow().css(i ? "left" : "top", 50 * (1 - t / e) + "%").css(i ? "top" : "left", "") }, m.prototype.setContent = function () { var t = this.tip(), e = this.getTitle(); this.options.html ? (this.options.sanitize && (e = n(e, this.options.whiteList, this.options.sanitizeFn)), t.find(".tooltip-inner").html(e)) : t.find(".tooltip-inner").text(e), t.removeClass("fade in top bottom left right") }, m.prototype.hide = function (t) { var e = this, i = g(this.$tip), o = g.Event("hide.bs." + this.type); function n() { "in" != e.hoverState && i.detach(), e.$element && e.$element.removeAttr("aria-describedby").trigger("hidden.bs." + e.type), t && t() } if (this.$element.trigger(o), !o.isDefaultPrevented()) return i.removeClass("in"), g.support.transition && i.hasClass("fade") ? i.one("bsTransitionEnd", n).emulateTransitionEnd(m.TRANSITION_DURATION) : n(), this.hoverState = null, this }, m.prototype.fixTitle = function () { var t = this.$element; (t.attr("title") || "string" != typeof t.attr("data-original-title")) && t.attr("data-original-title", t.attr("title") || "").attr("title", "") }, m.prototype.hasContent = function () { return this.getTitle() }, m.prototype.getPosition = function (t) { var e = (t = t || this.$element)[0], i = "BODY" == e.tagName, o = e.getBoundingClientRect(); null == o.width && (o = g.extend({}, o, { width: o.right - o.left, height: o.bottom - o.top })); var n = window.SVGElement && e instanceof window.SVGElement, s = i ? { top: 0, left: 0 } : n ? null : t.offset(), a = { scroll: i ? document.documentElement.scrollTop || document.body.scrollTop : t.scrollTop() }, r = i ? { width: g(window).width(), height: g(window).height() } : null; return g.extend({}, o, a, r, s) }, m.prototype.getCalculatedOffset = function (t, e, i, o) { return "bottom" == t ? { top: e.top + e.height, left: e.left + e.width / 2 - i / 2 } : "top" == t ? { top: e.top - o, left: e.left + e.width / 2 - i / 2 } : "left" == t ? { top: e.top + e.height / 2 - o / 2, left: e.left - i } : { top: e.top + e.height / 2 - o / 2, left: e.left + e.width } }, m.prototype.getViewportAdjustedDelta = function (t, e, i, o) { var n = { top: 0, left: 0 }; if (!this.$viewport) return n; var s = this.options.viewport && this.options.viewport.padding || 0, a = this.getPosition(this.$viewport); if (/right|left/.test(t)) { var r = e.top - s - a.scroll, l = e.top + s - a.scroll + o; r < a.top ? n.top = a.top - r : l > a.top + a.height && (n.top = a.top + a.height - l) } else { var h = e.left - s, d = e.left + s + i; h < a.left ? n.left = a.left - h : d > a.right && (n.left = a.left + a.width - d) } return n }, m.prototype.getTitle = function () { var t = this.$element, e = this.options; return t.attr("data-original-title") || ("function" == typeof e.title ? e.title.call(t[0]) : e.title) }, m.prototype.getUID = function (t) { for (; t += ~~(1e6 * Math.random()), document.getElementById(t);); return t }, m.prototype.tip = function () { if (!this.$tip && (this.$tip = g(this.options.template), 1 != this.$tip.length)) throw new Error(this.type + " `template` option must consist of exactly 1 top-level element!"); return this.$tip }, m.prototype.arrow = function () { return this.$arrow = this.$arrow || this.tip().find(".tooltip-arrow") }, m.prototype.enable = function () { this.enabled = !0 }, m.prototype.disable = function () { this.enabled = !1 }, m.prototype.toggleEnabled = function () { this.enabled = !this.enabled }, m.prototype.toggle = function (t) { var e = this; t && ((e = g(t.currentTarget).data("bs." + this.type)) || (e = new this.constructor(t.currentTarget, this.getDelegateOptions()), g(t.currentTarget).data("bs." + this.type, e))), t ? (e.inState.click = !e.inState.click, e.isInStateTrue() ? e.enter(e) : e.leave(e)) : e.tip().hasClass("in") ? e.leave(e) : e.enter(e) }, m.prototype.destroy = function () { var t = this; clearTimeout(this.timeout), this.hide(function () { t.$element.off("." + t.type).removeData("bs." + t.type), t.$tip && t.$tip.detach(), t.$tip = null, t.$arrow = null, t.$viewport = null, t.$element = null }) }, m.prototype.sanitizeHtml = function (t) { return n(t, this.options.whiteList, this.options.sanitizeFn) }; var e = g.fn.tooltip; g.fn.tooltip = function i(o) { return this.each(function () { var t = g(this), e = t.data("bs.tooltip"), i = "object" == typeof o && o; !e && /destroy|hide/.test(o) || (e || t.data("bs.tooltip", e = new m(this, i)), "string" == typeof o && e[o]()) }) }, g.fn.tooltip.Constructor = m, g.fn.tooltip.noConflict = function () { return g.fn.tooltip = e, this } }(jQuery), function (n) { "use strict"; var s = function (t, e) { this.init("popover", t, e) }; if (!n.fn.tooltip) throw new Error("Popover requires tooltip.js"); s.VERSION = "3.4.1", s.DEFAULTS = n.extend({}, n.fn.tooltip.Constructor.DEFAULTS, { placement: "right", trigger: "click", content: "", template: '<div class="popover" role="tooltip"><div class="arrow"></div><h3 class="popover-title"></h3><div class="popover-content"></div></div>' }), ((s.prototype = n.extend({}, n.fn.tooltip.Constructor.prototype)).constructor = s).prototype.getDefaults = function () { return s.DEFAULTS }, s.prototype.setContent = function () { var t = this.tip(), e = this.getTitle(), i = this.getContent(); if (this.options.html) { var o = typeof i; this.options.sanitize && (e = this.sanitizeHtml(e), "string" === o && (i = this.sanitizeHtml(i))), t.find(".popover-title").html(e), t.find(".popover-content").children().detach().end()["string" === o ? "html" : "append"](i) } else t.find(".popover-title").text(e), t.find(".popover-content").children().detach().end().text(i); t.removeClass("fade top bottom left right in"), t.find(".popover-title").html() || t.find(".popover-title").hide() }, s.prototype.hasContent = function () { return this.getTitle() || this.getContent() }, s.prototype.getContent = function () { var t = this.$element, e = this.options; return t.attr("data-content") || ("function" == typeof e.content ? e.content.call(t[0]) : e.content) }, s.prototype.arrow = function () { return this.$arrow = this.$arrow || this.tip().find(".arrow") }; var t = n.fn.popover; n.fn.popover = function e(o) { return this.each(function () { var t = n(this), e = t.data("bs.popover"), i = "object" == typeof o && o; !e && /destroy|hide/.test(o) || (e || t.data("bs.popover", e = new s(this, i)), "string" == typeof o && e[o]()) }) }, n.fn.popover.Constructor = s, n.fn.popover.noConflict = function () { return n.fn.popover = t, this } }(jQuery), function (s) { "use strict"; function n(t, e) { this.$body = s(document.body), this.$scrollElement = s(t).is(document.body) ? s(window) : s(t), this.options = s.extend({}, n.DEFAULTS, e), this.selector = (this.options.target || "") + " .nav li > a", this.offsets = [], this.targets = [], this.activeTarget = null, this.scrollHeight = 0, this.$scrollElement.on("scroll.bs.scrollspy", s.proxy(this.process, this)), this.refresh(), this.process() } function e(o) { return this.each(function () { var t = s(this), e = t.data("bs.scrollspy"), i = "object" == typeof o && o; e || t.data("bs.scrollspy", e = new n(this, i)), "string" == typeof o && e[o]() }) } n.VERSION = "3.4.1", n.DEFAULTS = { offset: 10 }, n.prototype.getScrollHeight = function () { return this.$scrollElement[0].scrollHeight || Math.max(this.$body[0].scrollHeight, document.documentElement.scrollHeight) }, n.prototype.refresh = function () { var t = this, o = "offset", n = 0; this.offsets = [], this.targets = [], this.scrollHeight = this.getScrollHeight(), s.isWindow(this.$scrollElement[0]) || (o = "position", n = this.$scrollElement.scrollTop()), this.$body.find(this.selector).map(function () { var t = s(this), e = t.data("target") || t.attr("href"), i = /^#./.test(e) && s(e); return i && i.length && i.is(":visible") && [[i[o]().top + n, e]] || null }).sort(function (t, e) { return t[0] - e[0] }).each(function () { t.offsets.push(this[0]), t.targets.push(this[1]) }) }, n.prototype.process = function () { var t, e = this.$scrollElement.scrollTop() + this.options.offset, i = this.getScrollHeight(), o = this.options.offset + i - this.$scrollElement.height(), n = this.offsets, s = this.targets, a = this.activeTarget; if (this.scrollHeight != i && this.refresh(), o <= e) return a != (t = s[s.length - 1]) && this.activate(t); if (a && e < n[0]) return this.activeTarget = null, this.clear(); for (t = n.length; t--;)a != s[t] && e >= n[t] && (n[t + 1] === undefined || e < n[t + 1]) && this.activate(s[t]) }, n.prototype.activate = function (t) { this.activeTarget = t, this.clear(); var e = this.selector + '[data-target="' + t + '"],' + this.selector + '[href="' + t + '"]', i = s(e).parents("li").addClass("active"); i.parent(".dropdown-menu").length && (i = i.closest("li.dropdown").addClass("active")), i.trigger("activate.bs.scrollspy") }, n.prototype.clear = function () { s(this.selector).parentsUntil(this.options.target, ".active").removeClass("active") }; var t = s.fn.scrollspy; s.fn.scrollspy = e, s.fn.scrollspy.Constructor = n, s.fn.scrollspy.noConflict = function () { return s.fn.scrollspy = t, this }, s(window).on("load.bs.scrollspy.data-api", function () { s('[data-spy="scroll"]').each(function () { var t = s(this); e.call(t, t.data()) }) }) }(jQuery), function (r) { "use strict"; var a = function (t) { this.element = r(t) }; function e(i) { return this.each(function () { var t = r(this), e = t.data("bs.tab"); e || t.data("bs.tab", e = new a(this)), "string" == typeof i && e[i]() }) } a.VERSION = "3.4.1", a.TRANSITION_DURATION = 150, a.prototype.show = function () { var t = this.element, e = t.closest("ul:not(.dropdown-menu)"), i = t.data("target"); if (i || (i = (i = t.attr("href")) && i.replace(/.*(?=#[^\s]*$)/, "")), !t.parent("li").hasClass("active")) { var o = e.find(".active:last a"), n = r.Event("hide.bs.tab", { relatedTarget: t[0] }), s = r.Event("show.bs.tab", { relatedTarget: o[0] }); if (o.trigger(n), t.trigger(s), !s.isDefaultPrevented() && !n.isDefaultPrevented()) { var a = r(document).find(i); this.activate(t.closest("li"), e), this.activate(a, a.parent(), function () { o.trigger({ type: "hidden.bs.tab", relatedTarget: t[0] }), t.trigger({ type: "shown.bs.tab", relatedTarget: o[0] }) }) } } }, a.prototype.activate = function (t, e, i) { var o = e.find("> .active"), n = i && r.support.transition && (o.length && o.hasClass("fade") || !!e.find("> .fade").length); function s() { o.removeClass("active").find("> .dropdown-menu > .active").removeClass("active").end().find('[data-toggle="tab"]').attr("aria-expanded", !1), t.addClass("active").find('[data-toggle="tab"]').attr("aria-expanded", !0), n ? (t[0].offsetWidth, t.addClass("in")) : t.removeClass("fade"), t.parent(".dropdown-menu").length && t.closest("li.dropdown").addClass("active").end().find('[data-toggle="tab"]').attr("aria-expanded", !0), i && i() } o.length && n ? o.one("bsTransitionEnd", s).emulateTransitionEnd(a.TRANSITION_DURATION) : s(), o.removeClass("in") }; var t = r.fn.tab; r.fn.tab = e, r.fn.tab.Constructor = a, r.fn.tab.noConflict = function () { return r.fn.tab = t, this }; var i = function (t) { t.preventDefault(), e.call(r(this), "show") }; r(document).on("click.bs.tab.data-api", '[data-toggle="tab"]', i).on("click.bs.tab.data-api", '[data-toggle="pill"]', i) }(jQuery), function (l) { "use strict"; var h = function (t, e) { this.options = l.extend({}, h.DEFAULTS, e); var i = this.options.target === h.DEFAULTS.target ? l(this.options.target) : l(document).find(this.options.target); this.$target = i.on("scroll.bs.affix.data-api", l.proxy(this.checkPosition, this)).on("click.bs.affix.data-api", l.proxy(this.checkPositionWithEventLoop, this)), this.$element = l(t), this.affixed = null, this.unpin = null, this.pinnedOffset = null, this.checkPosition() }; function i(o) { return this.each(function () { var t = l(this), e = t.data("bs.affix"), i = "object" == typeof o && o; e || t.data("bs.affix", e = new h(this, i)), "string" == typeof o && e[o]() }) } h.VERSION = "3.4.1", h.RESET = "affix affix-top affix-bottom", h.DEFAULTS = { offset: 0, target: window }, h.prototype.getState = function (t, e, i, o) { var n = this.$target.scrollTop(), s = this.$element.offset(), a = this.$target.height(); if (null != i && "top" == this.affixed) return n < i && "top"; if ("bottom" == this.affixed) return null != i ? !(n + this.unpin <= s.top) && "bottom" : !(n + a <= t - o) && "bottom"; var r = null == this.affixed, l = r ? n : s.top; return null != i && n <= i ? "top" : null != o && t - o <= l + (r ? a : e) && "bottom" }, h.prototype.getPinnedOffset = function () { if (this.pinnedOffset) return this.pinnedOffset; this.$element.removeClass(h.RESET).addClass("affix"); var t = this.$target.scrollTop(), e = this.$element.offset(); return this.pinnedOffset = e.top - t }, h.prototype.checkPositionWithEventLoop = function () { setTimeout(l.proxy(this.checkPosition, this), 1) }, h.prototype.checkPosition = function () { if (this.$element.is(":visible")) { var t = this.$element.height(), e = this.options.offset, i = e.top, o = e.bottom, n = Math.max(l(document).height(), l(document.body).height()); "object" != typeof e && (o = i = e), "function" == typeof i && (i = e.top(this.$element)), "function" == typeof o && (o = e.bottom(this.$element)); var s = this.getState(n, t, i, o); if (this.affixed != s) { null != this.unpin && this.$element.css("top", ""); var a = "affix" + (s ? "-" + s : ""), r = l.Event(a + ".bs.affix"); if (this.$element.trigger(r), r.isDefaultPrevented()) return; this.affixed = s, this.unpin = "bottom" == s ? this.getPinnedOffset() : null, this.$element.removeClass(h.RESET).addClass(a).trigger(a.replace("affix", "affixed") + ".bs.affix") } "bottom" == s && this.$element.offset({ top: n - t - o }) } }; var t = l.fn.affix; l.fn.affix = i, l.fn.affix.Constructor = h, l.fn.affix.noConflict = function () { return l.fn.affix = t, this }, l(window).on("load", function () { l('[data-spy="affix"]').each(function () { var t = l(this), e = t.data(); e.offset = e.offset || {}, null != e.offsetBottom && (e.offset.bottom = e.offsetBottom), null != e.offsetTop && (e.offset.top = e.offsetTop), i.call(t, e) }) }) }(jQuery);

