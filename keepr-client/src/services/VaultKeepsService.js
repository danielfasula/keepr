import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultKeepsService {
  async create(newVK) {
    try {
      const res = await api.post('api/vaultkeeps', newVK)
      logger.log(res.data)
    } catch (error) {
      logger.log(error)
    }
  }

  async removeKeepFromVault(vaultKeepId) {
    try {
      await api.delete('api/vaultkeeps/' + vaultKeepId)
    } catch (error) {
      logger.log(error)
    }
  }
}

export const vaultKeepsService = new VaultKeepsService()
