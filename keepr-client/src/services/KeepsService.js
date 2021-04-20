import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    try {
      const res = await api.get('api/keeps')
      AppState.keeps = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async getKeep(id) {
    try {
      const res = await api.get('api/keeps/' + id)
      AppState.activeKeep = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async createKeep(keepData) {
    try {
      const res = await api.post('api/keeps', keepData)
      AppState.keeps.push(res.data)
      return keepData
    } catch (error) {
      logger.log(error)
    }
  }

  // NOTE figure out how to splice out of appstate.keeps array
  async deleteKeep(keep) {
    try {
      const keepIndex = AppState.keeps.findIndex(k => k.id === keep.id)
      logger.log(keepIndex)
      await api.delete('api/keeps/' + keep.id)
      AppState.keeps.splice(keepIndex, 1)
      AppState.userKeeps.splice(keepIndex, 1)
    } catch (error) {
      logger.log(error)
    }
  }

  clearAK() {
    AppState.activeKeep = {}
  }
}

export const keepsService = new KeepsService()
