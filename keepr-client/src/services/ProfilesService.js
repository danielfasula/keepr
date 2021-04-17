import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfilesService {
  async getProfile(id) {
    try {
      const res = await api.get('api/profiles/' + id)
      AppState.profile = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async getProfileVaults(id) {
    try {
      const res = await api.get('api/profiles/' + id + '/vaults')
      AppState.profileVaults = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async getProfileKeeps(id) {
    try {
      const res = await api.get('api/profiles/' + id + '/keeps')
      AppState.profileKeeps = res.data
    } catch (error) {
      logger.log(error)
    }
  }
}

export const profilesService = new ProfilesService()
