<template>
  <h1>Bid Calculation Tool</h1>
  <form @submit.prevent="calculateFees">
    <div class="row">
      <div class="form-group col-md-6 mb-2">
        <label>Vehicle Type</label>
        <select v-model="auctionType" class="form-control form-control-sm">
          <option value="Common">Common</option>
          <option value="Luxury">Luxury</option>
        </select>
      </div>
      <div class="form-group col-md-6 mb-2">
        <label>Bid Amount</label>
        <input v-model="bidAmount" type="number" step="0.01" placeholder="Bid amount" class="form-control form-control-sm" />
      </div>
    </div>
    <div class="row">
      <button class="btn btn-primary mb-2">Calculate</button>
    </div>
  </form>
  <div v-if="calculated">
    <div class="row mb-2">
      <div class="col-md-3">Basic</div>
      <div class="col-md-3">Special</div>
      <div class="col-md-3">Association</div>
      <div class="col-md-3">Storage</div>
    </div>
    <div class="row mb-2">
      <div class="col-md-3">{{ $filters.currency(Basic) }}</div>
      <div class="col-md-3">{{ $filters.currency(Special) }}</div>
      <div class="col-md-3">{{ $filters.currency(Association) }}</div>
      <div class="col-md-3">{{ $filters.currency(Storage) }}</div>
    </div>
    <div class="row">
      <div class="col-md-12">
        <span class="fw-bold">Total: {{ $filters.currency(Total) }}</span>
      </div>
    </div>
  </div>
  <div v-if="error" v-html="errorMessage" class="alert alert-danger"></div>
</template>
<script>
  import axios from 'axios'
  export default {
    name: 'BidCalculator',
    data()
    {
      return {
        auctionType: 'Common',
        bidAmount: 0,
        Basic: 0,
        Special: 0,
        Association: 0,
        Storage: 0,
        Total: 0,
        calculated: false,
        error: false,
        errorMessage: ""
      }
    },
    methods: {
      async calculateFees() {
        this.calculated = false;
        this.error = false;
        let response = await axios.post("https://localhost:7046/BidCalculator", {
            auctionType: this.auctionType,
            bidAmount: this.bidAmount
        }).then(response => {
          this.Basic = response.data.Basic;
          this.Special = response.data.Special;
          this.Association = response.data.Association;
          this.Storage = response.data.Storage;
          this.Total = this.bidAmount + this.Basic + this.Special + this.Association + this.Storage;
          this.calculated = true;
        }).catch(error => {
          this.errorMessage = error;
          try {
            if (error.response.data.errors && typeof error.response.data.errors == "object") {
              this.errorMessage = "";
              for (const x in error.response.data.errors) {
                this.errorMessage += error.response.data.errors[x] + "<br />";
              }
            }
          } catch (error) { }
          this.error = true;
        });
      }
    }
  }
</script>
