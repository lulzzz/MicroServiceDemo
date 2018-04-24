import router from './router'
import store from './store'
import NProgress from 'nprogress' // Progress 进度条
import 'nprogress/nprogress.css'// Progress 进度条样式
import { Message } from 'element-ui'
import { getToken } from '@/utils/auth' // 验权

const whiteList = ['/login']; // 不重定向白名单
router.beforeEach((to, from, next) => {
    NProgress.start();
    getToken().then(() => {
            if (to.path === '/login') {
                next({ path: '/' });
            } else {
                if (store.getters.roles.length === 0) {
                    store.dispatch('GetInfo').then(res => { // 拉取用户信息
                        next();
                    }).catch(() => {
                        store.dispatch('FedLogOut').then(() => {
                            Message.error('验证失败,请重新登录');
                            next({ path: '/login' });
                        });
                    });
                } else {
                    next();
                }
            }
    }).catch(() => {
        if (whiteList.indexOf(to.path) !== -1) {
            console.log(5);
            next();
        } else {
            console.log(6);
            next('/login');
            NProgress.done();
        }
    });

  //  if (getToken()) {
  //      console.log(4);
  //  if (to.path === '/login') {
  //    next({ path: '/' });
  //  } else {
  //    if (store.getters.roles.length === 0) {
  //      store.dispatch('GetInfo').then(res => { // 拉取用户信息
  //        next();
  //      }).catch(() => {
  //        store.dispatch('FedLogOut').then(() => {
  //          Message.error('验证失败,请重新登录');
  //            next({ path: '/login' });
  //        });
  //      });
  //    } else {
  //      next();
  //    }
  //  }
  //} else {
  //      if (whiteList.indexOf(to.path) !== -1) {
  //          console.log(5);
  //    next();
  //      } else {
  //          console.log(6);
  //      next('/login');
  //      NProgress.done();
  //  }
  //}
});

router.afterEach(() => {
  NProgress.done(); // 结束Progress
});
