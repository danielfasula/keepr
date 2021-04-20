import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
  async getVault(id) {
    try {
      const res = await api.get('api/vaults/' + id)
      AppState.activeVault = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async getVaultKeeps(id) {
    try {
      const res = await api.get('api/vaults/' + id + '/keeps')
      AppState.vaultKeeps = res.data
    } catch (error) {
      logger.log(error)
    }
  }

  async createVault(vaultData) {
    try {
      const res = await api.post('api/vaults', vaultData)
      AppState.vaults.push(res.data)
      return res.data.id
    } catch (error) {
      logger.log(error)
    }
  }

  async deleteVault(id) {
    try {
      await api.delete('api/vaults/' + id)
    } catch (error) {
      logger.log(error)
    }
  }
}

export const vaultsService = new VaultsService()
