import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultKeepsService {
  async create(newVK) {
    try {
      await api.post('api/vaultkeeps', newVK)
    } catch (error) {
      logger.log(error)
    }
  }

  async removeKeepFromVault(vaultKeepId) {
    try {
      await api.delete('api/vaultkeeps/' + vaultKeepId)
      const keepIndex = AppState.vaultKeeps.findIndex(vk => vk.vaultKeepId === vaultKeepId)
      AppState.vaultKeeps.splice(keepIndex, 1)
    } catch (error) {
      logger.log(error)
    }
  }
}

export const vaultKeepsService = new VaultKeepsService()
