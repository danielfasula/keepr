import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getVaults() {
    try {
      const res = await api.get('/account/vaults')
      AppState.userVaults = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async getKeeps() {
    try {
      const res = await api.get('/account/keeps')
      AppState.userKeeps = res.data
    } catch (error) {
      logger.log(error)
    }
  }
}

export const accountService = new AccountService()
